using AutoMapper;
using CineLogic.Models;
using CineLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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