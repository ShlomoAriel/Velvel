using System;
using System.Linq;
using System.Security.Claims;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;
using Velvel.Domain.Tables.Plumbings;

namespace Velvel.Domain.Services
{
    public interface IPlumbingService : ITableService<Plumbing>
    {
        new Plumbing Update(ProjectEntity entity);
        new Plumbing Create(ProjectEntity entity);
        new void Create(Plumbing flooring);
    }
    public class PlumbingService : TableService<Plumbing>, IPlumbingService
    {
        private readonly IRepository<Plumbing> _plumbingRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        public PlumbingService(IRepository<MeasurementUnit> measurementRepository, IRepository<Room> roomRepository, IRepository<Accessory> accessoryRepository, IRepository<Plumbing> plumbingRepository, IRepository<Project> projectRepository, IRepository<CommentGroup> commentGroupRepository, IRepository<Comment> commentRepository)
            : base(plumbingRepository, measurementRepository, roomRepository, projectRepository, commentGroupRepository,commentRepository)
        {

            _accessoryRepository = accessoryRepository;
            _plumbingRepository = plumbingRepository;
        }


        public new Plumbing Create(ProjectEntity entity)
        {
            var entry = base.Create(entity);
            entry.Accessories = _accessoryRepository.TableNoTracking.ToList();

            return entry;
        }
        public new void Create(Plumbing entry)
        {
            entry.StatusId = 1;
            entry.Date = DateTime.Now;
            _plumbingRepository.Insert(entry);
        }
        public new Plumbing Update(ProjectEntity entity)
        {
            var entry = base.Update(entity.Id);

            if (entry == null)
                return null;

            entry.Accessories = _accessoryRepository.TableNoTracking.ToList();

            return entry;
        }
    }
}
