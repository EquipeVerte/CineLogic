using AutoMapper;
using CineLogic.Business.Contenus;
using CineLogic.Models;
using CineLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services.Description;

namespace CineLogic.Business.Programmation
{
    public class SeanceService : ISeanceService
    {
        #region MEMBRES
        private ISeanceRepository repository;

        // Mapping.
        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Seance, SeanceViewModel>()
                .ForMember(dest => dest.PrincipalFilm,
                opts => opts.MapFrom(src => src.SeanceContenus.Where(sc => sc.estPrincipal.GetValueOrDefault()).FirstOrDefault().ContenuTitre))
                .ForMember(dest => dest.Contenus,
                opts => opts.MapFrom(src => src.SeanceContenus.Concat<ISeanceContenu>(src.SeancePromoes)));
            cfg.CreateMap<SeanceViewModel, Seance>();
            cfg.CreateMap<ISeanceContenu, ContenuViewModel>()
                .ForMember(dest => dest.RuntimeMins,
                opts => opts.MapFrom(src => src.GetContenu().RuntimeMins))
                .ForMember(dest => dest.Typage,
                opts => opts.MapFrom(src => src.GetContenu().typage));
        })
            .CreateMapper();
        #endregion

        #region CONSTRUCTORS
        public SeanceService()
        {
            repository = new EFSeanceRepository();
        }

        public SeanceService(ISeanceRepository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region LECTURE
        //  Chercher tous les séances planifiées pour une salle.
        public IEnumerable<SeanceViewModel> GetSeancesBySalle(int salleID)
        {
            return mapper.Map<IEnumerable<Seance>, IEnumerable<SeanceViewModel>>(repository.GetSeancesBySalle(salleID));
        }

        //  Chercher une séance specifique par ID.
        public SeanceViewModel GetSeance(int id)
        {
            Seance seance = repository.GetSeance(id);

            if (seance == null)
            {
                throw new NotFoundException($"Le séance avec ID {id} n'existe pas dans la base de données.");
            }

            SeanceViewModel svm = mapper.Map<Seance, SeanceViewModel>(seance);

            //  Chercher les contenus de tous les types et les mettre dans une liste.
            //svm.Contenus =
            //    mapper.Map<IEnumerable<SeanceContenu>, IEnumerable<ContenuViewModel>>(seance.SeanceContenus)
            //        .Concat(mapper.Map<IEnumerable<SeancePromo>, IEnumerable<ContenuViewModel>>(seance.SeancePromoes))
            //        .ToList();

            //  Trier les contenus en ordre d'affichage.
            svm.Contenus.Sort();

            return svm;
        }
        #endregion

        #region CRÉATION
        //  Validation et création d'une séance.
        public SeanceViewModel CreateSeance(SeanceViewModel seanceVM)
        {
            if (seanceVM.Validate(this))
            {
                Seance seance = mapper.Map<SeanceViewModel, Seance>(seanceVM);

                try
                {
                    repository.CreateSeance(seance);
                }
                catch (Exception ex)
                {
                    throw new DBException(ex);
                }

                return mapper.Map<Seance, SeanceViewModel>(seance);
            }
            else
            {
                throw new ScheduleException();
            }
        }
        #endregion

        #region ÉDITION
        //  Validation et mise à jour d'une séance dans la base de données.
        public SeanceViewModel UpdateSeance(SeanceViewModel seanceVM)
        {
            if (seanceVM.Validate(this))
            {
                Seance seance = mapper.Map<SeanceViewModel, Seance>(seanceVM);

                try
                {
                    repository.UpdateSeance(seance);
                }
                catch (Exception ex)
                {
                    throw new DBException(ex);
                }

                return mapper.Map<Seance,SeanceViewModel>(seance);
            }
            else
            {
                throw new ScheduleException();
            }
        }

        //  Mettre à jour les contenus d'une séance - le contenu principal et l'ordre d'affichage.
        public void UpdateSeanceContents(SeanceViewModel seanceVM)
        {
            Seance seance = repository.GetSeance(seanceVM.SeanceID);

            //  Mettre à jour les contenus seulement s'il y'en a.
            if(seance.SeanceContenus.Count > 0 || seance.SeancePromoes.Count > 0)
            {
                List<string> order = new List<string>(seanceVM.Order.Split(','));

                //  Traiter les contenu standard et court.
                foreach (var content in seance.SeanceContenus)
                {
                    content.indexOrdre = order.IndexOf(content.ContenuTitre);

                    if (content.ContenuTitre == seanceVM.PrincipalFilm)
                    {
                        content.estPrincipal = true;
                    }
                    else
                    {
                        content.estPrincipal = false;
                    }
                }

                //  Traiter les promos.
                foreach (var content in seance.SeancePromoes)
                {
                    content.indexOrdre = order.IndexOf(content.PromoTitre);
                }
            }
        }

        //  Ajuster la durée d'une séance à son contenu.
        public void AdjustTimeToContent(int seanceID)
        {
            Seance seance = repository.GetSeance(seanceID);

            if (seance != null)
            {
                //  Faire l'ajustement seulement s'il y a des contenus.
                if (seance.SeanceContenus.Count > 0)
                {
                    int totalRuntime = 0;

                    DateTime oldFin = seance.HeureFin;

                    totalRuntime += seance.SeanceContenus.Sum(sc => sc.Contenu.RuntimeMins);

                    totalRuntime += seance.SeancePromoes.Sum(sc => sc.ContenuPromo.RuntimeMins);

                    totalRuntime += (5 - totalRuntime % 5);

                    seance.HeureFin = seance.HeureDebut.AddMinutes(totalRuntime);

                    if (!mapper.Map<Seance, SeanceViewModel>(seance).Validate(this))
                    {
                        seance.HeureFin = oldFin;
                        throw new ScheduleException();
                    }

                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                throw new NotFoundException($"Le séance avec ID {seanceID} n'existe pas dans la base de données.");
            }
        }

        public void UpdateSeanceTimes(SeanceViewModel seanceVM)
        {
            if (seanceVM.Validate(this))
            {
                Seance seance = repository.GetSeance(seanceVM.SeanceID);

                seance.HeureDebut = seanceVM.HeureDebut;
                seance.HeureFin = seanceVM.HeureFin;
            }
            else
            {
                throw new ScheduleException();
            }
        }

        public void AddContentToSeance(int seanceID, string contenuTitre)
        {
            // TODO adding content to seance logic.

            // If first standard content it becomes principal film.
            // Film added at the end of order.

            string type = repository.GetContentType(contenuTitre);

            if (type == "notfound")
            {
                throw new NotFoundException($"Le contenu avec le titre {contenuTitre} n'existe pas dans la base de données.");
            }

            Seance seance = repository.GetSeance(seanceID);

            List<ContenuViewModel> contents =
                    mapper.Map<IEnumerable<SeanceContenu>, IEnumerable<ContenuViewModel>>(seance.SeanceContenus)
                        .Concat(mapper.Map<IEnumerable<SeancePromo>, IEnumerable<ContenuViewModel>>(seance.SeancePromoes))
                        .ToList();

            if(contents.Any(c => c.ContenuTitre == contenuTitre))
            {
                throw new DuplicateContentException(contenuTitre);
            }

            if (type != ContenuTypeLibrary.CONT_TYPE_PROMO)
            {
                SeanceContenu seanceContenu = new SeanceContenu();
                seanceContenu.ContenuTitre = contenuTitre;
                seanceContenu.indexOrdre = contents.Count == 0 ? 0 : contents.Max(c => c.indexOrdre) + 1;
                seanceContenu.SeanceID = seance.SeanceID;

                if (seance.SeanceContenus.Where(sc => sc.Contenu.typage == ContenuTypeLibrary.CONT_TYPE_STANDARD).Count() == 0)
                {
                    seanceContenu.estPrincipal = true;
                }

                repository.AddContenu(seanceContenu);
            }
            else
            {
                SeancePromo seancePromo = new SeancePromo();
                seancePromo.PromoTitre = contenuTitre;
                seancePromo.indexOrdre = contents.Count == 0 ? 0 : contents.Max(c => c.indexOrdre) + 1;
                seancePromo.SeanceID = seance.SeanceID;

                repository.AddPromo(seancePromo);
            }
        }

        public void DeleteContentFromSeance(int seanceID, string contenuTitre)
        {
            Seance seance = repository.GetSeance(seanceID);

            List<ContenuViewModel> contents =
                    mapper.Map<IEnumerable<SeanceContenu>, IEnumerable<ContenuViewModel>>(seance.SeanceContenus)
                        .Concat(mapper.Map<IEnumerable<SeancePromo>, IEnumerable<ContenuViewModel>>(seance.SeancePromoes))
                        .ToList();

            contents.Sort();

            int index = 0;
            bool passed = false;

            foreach (var content in contents)
            {
                if (content.ContenuTitre == contenuTitre)
                {
                    passed = true;

                    if (content.Typage == ContenuTypeLibrary.CONT_TYPE_PROMO)
                    {
                        seance.SeancePromoes.Remove(seance.SeancePromoes.Where(sp => sp.PromoTitre == contenuTitre).FirstOrDefault());
                    }
                    else
                    {
                        SeanceContenu c = seance.SeanceContenus.Where(sc => sc.ContenuTitre == contenuTitre).FirstOrDefault();

                        if (c.estPrincipal.GetValueOrDefault())
                        {
                            SeanceContenu newPrincipal = seance.SeanceContenus.Where(sc => sc.Contenu.typage == ContenuTypeLibrary.CONT_TYPE_STANDARD).LastOrDefault();

                            if (newPrincipal != null)
                            {
                                newPrincipal.estPrincipal = true;
                            }
                        }

                        seance.SeanceContenus.Remove(c);
                    }
                }

                if (passed)
                {
                    content.indexOrdre = content.indexOrdre - 1;
                }

                index++;
            }
        }
        #endregion

        #region SUPRESSION
        public void DeleteSeance(int id)
        {
            Seance seance = repository.GetSeance(id);

            if (seance != null)
            {
                try
                {
                    repository.DeleteSeance(seance);
                }
                catch (Exception ex)
                {
                    throw new DBException(ex);
                }
            }
            else
            {
                throw new NotFoundException($"La séance avec ID {id} n'existe pas dans la base de données.");
            }
        }
        #endregion

        #region TRANSACTIONS
        public void SaveChanges()
        {
            repository.SaveChanges();
        }

        public void Annuler()
        {
            repository.Annuler();
        }

        public void Dispose()
        {
            if (repository != null)
            {
                repository.Dispose();
            }
            return;
        }
        #endregion
    }
}