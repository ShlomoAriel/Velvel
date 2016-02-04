using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Models.Project.Attribute;

namespace Velvel.Controllers
{
    public class RoomController : AttributeController<Room, RoomViewModel>
    {
        public RoomController(IProjectEntityService<Room> baseService, IProjectService projectService)
            : base(baseService, projectService)
        {
        }
    }
}