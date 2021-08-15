using Education.Areas.Admin.Data.DataModel;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Areas.Admin.Data.BusinessModel.Interface
{
    interface IBatchModelService
    {
        ClassRoom convertBatch(BatchModel model, int id);
        BatchModel convertBatchModel(ClassRoom batch);
        ClassRoom convertEditBatch(BatchModel model, ClassRoom batch);
    }
}
