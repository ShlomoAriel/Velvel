using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Domain.Projects;

namespace Velvel.Domain.Services
{
    public interface ITableService<T> : IProjectEntityService<T> where T : Table
    {
        new T Create(ProjectEntity entity);
        new void Create(T changes);
        new T Update(ProjectEntity entity);
        new T GetById(int id);
        void AddComment(Comment comment);
        List<Comment> GetComments(int groupId);

    }
    public class TableService<T> : ProjectEntityService<T>, ITableService<T> where T : Table, new()
    {
        private readonly IRepository<T> _tableRepository;
        private readonly IRepository<MeasurementUnit> _measurementRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<CommentGroup> _commentGroupRepository;
        private readonly IRepository<Comment> _commentRepository;


        public TableService(IRepository<T> tableRepository, IRepository<MeasurementUnit> measurementRepository, IRepository<Room> roomRepository, IRepository<Project> projectRepository, IRepository<CommentGroup> commentGroupRepository, IRepository<Comment> commentRepository)
            : base(tableRepository, projectRepository)
        {
            _tableRepository = tableRepository;
            _measurementRepository = measurementRepository;
            _roomRepository = roomRepository;
            _commentGroupRepository = commentGroupRepository;
            _commentRepository = commentRepository;
        }
        public new T Update(ProjectEntity entity)
        {
            var entry = GetById(entity.Id);
            if (entity.ProjectId == null)
                return null;

            entry.MeasurementUnits = _measurementRepository.TableNoTracking.ToList();
            entry.Rooms = _roomRepository.TableNoTracking.Where(x => x.ProjectId == entity.ProjectId || x.CustomerId == entity.CustomerId || x.ProjectId == null).ToList();
            return entry;
        }

        public new T GetById(int id)
        {
            var entry = base.GetById(id);
            entry.Comments = GetComments(entry.CommentGroupId);
            return entry;
        }

        public void AddComment(Comment comment)
        {
            comment.Date=DateTime.Now;
            _commentRepository.Insert(comment);
        }

        public List<Comment> GetComments(int groupId)
        {
            return _commentRepository.Table.Where(x => x.CommentGroupId == groupId).ToList();
        }

        public new void Create(T entry)
        {
            var commentGroup = new CommentGroup();
            _commentGroupRepository.Insert(commentGroup);
            entry.CommentGroupId = commentGroup.Id;
            entry.StatusId = 1;
            entry.Date = DateTime.Now;
            _tableRepository.Insert(entry);
        }
        public new T Create(ProjectEntity entity)
        {
            var projectEntity = base.Create(entity);
            if (projectEntity == null) 
                return null;
            //projectEntity.Comments = GetComments(projectEntity.CommentGroupId);
            projectEntity.MeasurementUnits = _measurementRepository.TableNoTracking.ToList();
            projectEntity.Rooms = _roomRepository.TableNoTracking.Where(x => x.ProjectId == entity.ProjectId || x.CustomerId == entity.CustomerId || x.ProjectId == null).ToList();

            return projectEntity;
        }
    }
}
