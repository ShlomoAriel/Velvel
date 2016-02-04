using System;
using System.Linq;
using System.Security.Claims;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;
using Velvel.Domain.Tables.Floorings;

namespace Velvel.Domain.Services
{
    public interface IFlooringService : ITableService<Flooring>
    {
        new void Create(Flooring flooring);
        new Flooring Update(ProjectEntity entity);
        new Flooring Create(ProjectEntity entity);
    }
    public class FlooringService : TableService<Flooring>, IFlooringService
    {
        private readonly IRepository<Flooring> _flooringRepository;
        private readonly IRepository<Grout> _groutRepository;
        private readonly IRepository<Model> _modelRepository;
        public FlooringService(IRepository<MeasurementUnit> measurementRepository, IRepository<Room> roomRepository, IRepository<Grout> groutRepository, IRepository<Model> modelRepository, IRepository<Flooring> flooringRepository, IRepository<Project> projectRepository, IRepository<CommentGroup> commentGroupRepository, IRepository<Comment> commentRepository)
            : base(flooringRepository, measurementRepository, roomRepository, projectRepository, commentGroupRepository,commentRepository)
        {

            _groutRepository = groutRepository;
            _modelRepository = modelRepository;
            _flooringRepository = flooringRepository;
        }
        public new Flooring Create(ProjectEntity entity)
        {
            var entry = base.Create(entity);
            entry.Models = _modelRepository.TableNoTracking.ToList();
            entry.Grouts = _groutRepository.TableNoTracking.ToList();

            return entry;
        }

        public new void Create(Flooring entry)
        {
            entry.StatusId = 1;
            entry.Date = DateTime.Now;
            _flooringRepository.Insert(entry);
        }
        public new Flooring Update(ProjectEntity entity)
        {
            var entry = base.Update(entity);

            if (entry == null)
                return null;

            entry.Grouts = _groutRepository.TableNoTracking.ToList();
            entry.Models = _modelRepository.TableNoTracking.ToList();

            return entry;
        }
    }
}
