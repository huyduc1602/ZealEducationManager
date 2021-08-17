using Education.Areas.Admin.Data.BusinessModel.Interface;
using Education.Areas.Admin.Data.DataModel;
using Education.Areas.Admin.Data.ViewModel;
using Education.BLL;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.BusinessModel
{
    public class DbFaultyModelService : IFaultyModelService
    {
        private IRepository<GroupUser> groupUserRepository;
        private IRepository<User> userRepository;
        public DbFaultyModelService()
        {
            groupUserRepository = new DbRepository<GroupUser>();
            userRepository = new DbRepository<User>();
        }
        public Faulty ConvertFaulty(FaultyModel model)
        {
            Faulty faulty = new Faulty
            {
                Code = model.Code,
                Name = model.Name,
                Email = model.Email,
                Gender = model.Gender == 0 ? false : true,
                Phone = model.Phone,
                Address = model.Address,
                Birthday = model.Birthday,
                Qualification = model.Qualification,
                Salary = model.Salary,
                CreatedAt = DateTime.Today,
            };
            return faulty;
        }

        public FaultyModel ConvertFaultyModel(Faulty faulty, User user)
        {
            FaultyModel model = new FaultyModel
            {
                Id = faulty.Id,
                Code = faulty.Code,
                Name = faulty.Name,
                Email = faulty.Email,
                Gender = faulty.Gender == false ? 0 : 1,
                Phone = faulty.Phone,
                Address = faulty.Address,
                Birthday = faulty.Birthday,
                Qualification = faulty.Qualification,
                Salary = faulty.Salary,
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                ImageDisplay = faulty.Image,
            };
            return model;
        }

        public List<FaultyView> ConvertListFaultyView(List<Faulty> list)
        {
            List<FaultyView> views = new List<FaultyView>();
            foreach (var model in list)
            {
                FaultyView faultyView = ConvertFaultyView(model);
                views.Add(faultyView);
            }
            return views;
        }

        public FaultyView ConvertFaultyView(Faulty entity)
        {
            FaultyView faultyView = new FaultyView
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Image = entity.Image,
                Gender = entity.Gender,
                Address = entity.Address,
                Qualification = entity.Qualification,
                Salary = entity.Salary,
                UserName = userRepository.FindById(entity.UserId).UserName,
                Birthday = entity.Birthday,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt ?? null,
            };
            return faultyView;
        }

        public User ConvertUser(FaultyModel model)
        {
            GroupUser groupUser = groupUserRepository.Get(x => x.Name.Equals("Faulty")).First();
            User user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                FullName = model.FullName,
                GroupUserId = groupUser.Id,
                CreatedDate = DateTime.Today,
            };
            return user;
        }

        public Faulty ConvertFaulty(FaultyModel model, Faulty faulty)
        {
            faulty.Code = model.Code;
            faulty.Name = model.Name;
            faulty.Email = model.Email;
            faulty.Gender = model.Gender == 0 ? false : true;
            faulty.Phone = model.Phone;
            faulty.Address = model.Address;
            faulty.Birthday = model.Birthday;
            faulty.Qualification = model.Qualification;
            faulty.Salary = model.Salary;
            faulty.UpdatedAt = DateTime.Today;
            return faulty;
        }

        public User ConvertUser(FaultyModel model, User user)
        {
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.UpdatedDate = DateTime.Today;
            return user;
        }
    }
}