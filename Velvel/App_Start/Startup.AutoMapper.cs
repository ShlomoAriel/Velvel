using System.Web.Mvc;
using AutoMapper;
using Velvel.Domain.Attributes;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Tables.ChangesAndAdditions;
using Velvel.Domain.Tables.Defects;
using Velvel.Domain.Tables.Floorings;
using Velvel.Domain.Tables.Plumbings;
using Velvel.Models;
using Velvel.Models.Project.Table;
using Velvel.Models.Project.Table.Changes;
using Velvel.Models.Project.Table.Defects;
using Velvel.Models.Project.Table.Floorings;
using Velvel.Models.Project.Table.Plumbing;
using Velvel.Domain.Data;
using Velvel.Domain.Users.Customers;
using Velvel.Domain.Users.Managers;
using Velvel.Models.Project;
using Velvel.Models.Project.Attribute;

namespace IdentitySample
{
    public partial class Startup
    {
        public void ConfigureAutoMapper()
        {
            //Mapper.CreateMap<Customer, CustomerViewModel>().ReverseMap();

            Mapper.CreateMap<Customer, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Email))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Project, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Manager, SelectListItem>()
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id))
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Email));
            Mapper.CreateMap<MeasurementUnit, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Room, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Grout, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            //Mapper.CreateMap<Measurement, SelectListItem>()
            //    .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
            //    .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Model, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Accessory, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Status, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Status, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<MeasurementUnit, SelectListItem>()
                .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
                .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));

        //    Mapper.CreateMap<User, SelectListItem>()
        //        .ForMember(d => d.Text, s => s.MapFrom(x => string.Format("{0} {1}",x.FirstName,x.LastName)))
        //        .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));

        //    Mapper.CreateMap<Project, SelectListItem>()
        //        .ForMember(d => d.Text, s => s.MapFrom(x => x.Name))
        //        .ForMember(d => d.Value, s => s.MapFrom(x => x.Id));
            Mapper.CreateMap<Flooring, FlooringViewModel>().ReverseMap();
            //Mapper.CreateMap<File, FileViewModel>().ReverseMap();
            Mapper.CreateMap<Plumbing, PlumbingViewModel>().ReverseMap();
            Mapper.CreateMap<Accessory, AccessoryViewModel>().ReverseMap();
            Mapper.CreateMap<Defect, DefectViewModel>().ReverseMap();
            //Mapper.CreateMap<Status, StatusViewModel>().ReverseMap();
            Mapper.CreateMap<Grout, GroutViewModel>().ReverseMap();
            Mapper.CreateMap<AttributeEntity, PricedViewModel>().ReverseMap();
            Mapper.CreateMap<ProjectEntity, ProjectEntityViewModel>().ReverseMap();
            Mapper.CreateMap<Model, ModelViewModel>().ReverseMap();
            Mapper.CreateMap<Table, TableViewModel>().ReverseMap();
            //Mapper.CreateMap<Measurement, MeasurementViewModel>().ReverseMap();

            Mapper.CreateMap<Room, ProjectEntityViewModel>().ReverseMap();
            Mapper.CreateMap<Changes, ChangeViewModel>().ReverseMap();
            Mapper.CreateMap<Changes, TableViewModel>().ReverseMap();
            Mapper.CreateMap<Defect, TableViewModel>().ReverseMap();
            //Mapper.CreateMap<Manager, ManagerViewModel>().ReverseMap();
            //Mapper.CreateMap<UnitType, UnitTypeViewModel>().ReverseMap();
            //Mapper.CreateMap<State, StateViewModel>().ReverseMap();
            Mapper.CreateMap<Project, ProjectViewModel>()
                .ReverseMap();
            Mapper.CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();
                //.ForMember(d => d.CustomerName, s => s.MapFrom(x => x.Customer.Email))
                //.ForMember(d => d.ManagerName, s => s.MapFrom(x => x.Manager.Email)).ReverseMap(); 
            //Mapper.CreateMap<Customer, CustomerViewModel>().ReverseMap();
        //    Mapper.CreateMap<Task, CreateTaskViewModel>().ForMember(x => x.AvailableCustomers, y => y.Ignore()).
        //        ForMember(x => x.AvailableProjects, y => y.Ignore()).ReverseMap();

        //    Mapper.CreateMap<Task, TaskViewModel>()
        //        .ForMember(d => d.CustomerName, s => s.MapFrom(x => x.Customer.Name))
        //        .ForMember(d => d.ProjectName, s => s.MapFrom(x => x.Project.Name)).ReverseMap();

        //    Mapper.CreateMap<Report, ReportModel>().ReverseMap();
        //    Mapper.CreateMap<User, UserViewModel>().ForMember(s => s.Password, d => d.Ignore()).
        //        ForMember(s => s.PasswordConfirm, d => d.Ignore()).
        //        ForMember(s => s.UserRoles, d => d.Ignore()).
        //        ForMember(s => s.UserRoleId, d => d.Ignore());
        }
    }
}