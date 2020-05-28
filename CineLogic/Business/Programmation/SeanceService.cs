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
        private ISeanceRepository repository;

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Seance, SeanceViewModel>();
            cfg.CreateMap<Seance, SeanceEditionViewModel>();
            cfg.CreateMap<SeanceViewModel, Seance>();
            cfg.CreateMap<SeanceContenu, ContenuViewModel>()
                .ForMember(dest => dest.RuntimeMins,
                opts => opts.MapFrom(src => src.Contenu.RuntimeMins))
                .ForMember(dest => dest.Typage,
                opts => opts.MapFrom(src => src.Contenu.typage));
            cfg.CreateMap<SeancePromo, ContenuViewModel>()
                .ForMember(dest => dest.RuntimeMins,
                opts => opts.MapFrom(src => src.ContenuPromo.RuntimeMins))
                .ForMember(dest => dest.Typage,
                opts => opts.MapFrom(src => "promo"));
        }).CreateMapper();

        public SeanceService()
        {
            repository = new EFSeanceRepository();
        }

        public SeanceService(ISeanceRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<SeanceViewModel> GetSeancesBySalle(int salleID)
        {
            return mapper.Map<IEnumerable<Seance>, IEnumerable<SeanceViewModel>>(repository.GetSeancesBySalle(salleID));
        }

        public SeanceViewModel GetSeance(int id)
        {
            Seance seance = repository.GetSeance(id);

            if (seance != null)
            {
                return mapper.Map<Seance, SeanceViewModel>(seance);
            }

            throw new NotFoundException($"Le séance avec ID {id} n'existe pas dans la base de données.");
        }

        public SeanceEditionViewModel GetEditableSeance(int id)
        {
            Seance seance = repository.GetSeance(id);

            if (seance != null)
            {
                SeanceEditionViewModel sevm =  mapper.Map<Seance, SeanceEditionViewModel>(seance);

                sevm.Contenus =
                    mapper.Map<IEnumerable<SeanceContenu>, IEnumerable<ContenuViewModel>>(seance.SeanceContenus)
                        .Concat(mapper.Map<IEnumerable<SeancePromo>, IEnumerable<ContenuViewModel>>(seance.SeancePromoes))
                        .ToList();

                return sevm;
            }

            throw new NotFoundException($"Le séance avec ID {id} n'existe pas dans la base de données.");
        }

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

        public void AdjustTimeToContent(int seanceID)
        {
            Seance seance = repository.GetSeance(seanceID);

            if (seance != null)
            {
                if (seance.SeanceContenus.Count > 0)
                {
                    int totalRuntime = 0;

                    totalRuntime += seance.SeanceContenus.Sum(sc => sc.Contenu.RuntimeMins);

                    totalRuntime += seance.SeancePromoes.Sum(sc => sc.ContenuPromo.RuntimeMins);

                    seance.HeureFin = seance.HeureDebut.AddMinutes(totalRuntime);

                    if (mapper.Map<Seance, SeanceEditionViewModel>(seance).Validate(this))
                    {
                        repository.UpdateSeance(seance);
                    }
                    else
                    {
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

            Seance seance = repository.GetSeance(seanceID);

            SeanceContenu seanceContenu = new SeanceContenu();
            seanceContenu.ContenuTitre = contenuTitre;
            seanceContenu.indexOrdre = seance.SeanceContenus.Max(sc => sc.indexOrdre) > seance.SeancePromoes.Max(sp => sp.indexOrdre) ? seance.SeanceContenus.Max(sc => sc.indexOrdre) : seance.SeancePromoes.Max(sp => sp.indexOrdre);
            seanceContenu.SeanceID = seance.SeanceID;

            if (seance.SeanceContenus.Where(sc => sc.Contenu.typage == "standard").Count() == 0)
            {
                seanceContenu.estPrincipal = true;
            }

            repository.AddContenu(seanceContenu);
        }

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
    }
}