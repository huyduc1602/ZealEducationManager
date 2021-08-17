using Education.Areas.Admin.Data.DataModel;
using Education.Areas.Admin.Data.ViewModel;
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
        BatchModel convertBatchModel(ClassRoom batch, int FaultyId);
        ClassRoom convertEditBatch(BatchModel model, ClassRoom batch);
        IEnumerable<BatchView> convertListBatchView(IEnumerable<ClassRoom> list);
        BatchView convertBatchView(ClassRoom room);
    }
}
