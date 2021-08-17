using Education.Areas.Admin.Data.DataModel;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Areas.Admin.Data.BusinessModel.Interface
{
    interface ICandicateModelService
    {
        Candicate ConvertCandicate(CandicateModel model);
        User ConvertUser(CandicateModel model);
        Candicate ConvertEditCandicate(CandicateModel model, Candicate candicate);
        User ConvertEditUser(CandicateModel model, User user);
        CandicateModel ConvertCandicateModel(Candicate candicate, User user);
    }
}
