using Education.Areas.Admin.Data.BusinessModel.Interface;
using Education.Areas.Admin.Data.DataModel;
using Education.BLL;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.BusinessModel
{
    public class DbCandicateModelService : ICandicateModelService
    {
        private IRepository<GroupUser> groupUserRepository;
        public DbCandicateModelService()
        {
            groupUserRepository = new DbRepository<GroupUser>();
        }
        public Candicate ConvertCandicate(CandicateModel model)
        {
            Candicate student = new Candicate
            {
                Code = model.Code,
                Name = model.Name,
                Email = model.Email,
                ParentName = model.ParentName,
                ParentPhone = model.ParentPhone,
                Gender = model.Gender == 0 ? false : true,
                Phone = model.Phone,
                Address = model.Address,
                Birthday = model.Birthday,
                JoiningDate = model.JoiningDate,
                CreatedAt = DateTime.Today,
            };
            return student;
        }

        public CandicateModel ConvertCandicateModel(Candicate candicate, User user)
        {
            CandicateModel model = new CandicateModel
            {
                Id = candicate.Id,
                Code = candicate.Code,
                Name = candicate.Name,
                Email = candicate.Email,
                ParentName = candicate.ParentName,
                ParentPhone = candicate.ParentPhone,
                Gender = candicate.Gender == false ? 0 : 1,
                Phone = candicate.Phone,
                Address = candicate.Address,
                Birthday = candicate.Birthday,
                JoiningDate = candicate.JoiningDate,
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                ImageDisplay = candicate.Image,
            };
            return model;
        }

        public Candicate ConvertEditCandicate(CandicateModel model, Candicate candicate)
        {
            candicate.Code = model.Code;
            candicate.Name = model.Name;
            candicate.Email = model.Email;
            candicate.ParentName = model.ParentName;
            candicate.ParentPhone = model.ParentPhone;
            candicate.Gender = model.Gender == 0 ? false : true;
            candicate.Phone = model.Phone;
            candicate.Address = model.Address;
            candicate.Birthday = model.Birthday;
            candicate.JoiningDate = model.JoiningDate;
            candicate.UpdatedAt = DateTime.Today;
            return candicate;
        }

        public User ConvertEditUser(CandicateModel model, User user)
        {
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.UpdatedDate = DateTime.Today;
            return user;
        }

        public User ConvertUser(CandicateModel model)
        {

            GroupUser groupUser = groupUserRepository.Get(x => x.Name.Equals("Candicate")).First();
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
    }
}