using AutoMapper;
using CineLogic.Models;
using CineLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class SeanceService : ISeanceService
    {
        private ISeanceRepository repository;

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Seance, SeanceViewModel>();
            cfg.CreateMap<SeanceViewModel, Seance>();
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

        public SeanceViewModel CreateSeance(SeanceViewModel seanceVM)
        {
            if (seanceVM.Validate(this))
            {
                Seance seance = mapper.Map<SeanceViewModel, Seance>(seanceVM);

                try
                {
                    repository.CreateSeance(seance);

                    repository.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new DBException(ex);
                }

                return mapper.Map<Seance, SeanceViewModel>(seance);
            }
            else
            {
                throw new ScheduleException("Les heures de début et fin ne sont pas valides. Vérifier que l'heure de début est avant l'heure de fin et qu'il n'y a pas de conflits pour la salle.");
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

                    repository.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new DBException(ex);
                }

                return mapper.Map<Seance,SeanceViewModel>(seance);
            }
            else
            {
                throw new ScheduleException("Les heures de début et fin ne sont pas valides. Vérifier que l'heure de début est avant l'heure de fin et qu'il n'y a pas de conflits pour la salle.");
            }
        }

        public void DeleteSeance(int id)
        {
            Seance seance = repository.GetSeance(id);

            if (seance != null)
            {
                try
                {
                    repository.DeleteSeance(seance);

                    repository.SaveChanges();
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