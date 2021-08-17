using Education.Areas.Admin.Data.BusinessModel.Interface;
using Education.Areas.Admin.Data.DataModel;
using Education.Areas.Admin.Data.ViewModel;
using Education.BLL;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Education.Areas.Admin.Data.BusinessModel
{
    public class DbBatchModelService : IBatchModelService
    {
        private IRepository<ClassRoom> batchRepository;
        private IRepository<Course> courseRepository;
        public DbBatchModelService()
        {
            batchRepository = new DbRepository<ClassRoom>();
            courseRepository = new DbRepository<Course>();
        }
        public ClassRoom convertBatch(BatchModel model, int id)
        {
            string code;
            do
            {
                code = GetCode();
            } while (batchRepository.CheckDuplicate(x => x.Code.Equals(code)));
            Course course = courseRepository.FindById(model.CourseId);
            var EndDate = GetDateTime(model.StartDate, course.StudyTime);
            ClassRoom batch = new ClassRoom
            {
                Code = code,
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = EndDate,
                CourseId = model.CourseId,
                Status = model.Status,
                UserId = id,
                CreatedAt = DateTime.Today,
            };
            return batch;
        }

        public BatchModel convertBatchModel(ClassRoom batch, int FaultyId)
        {
            BatchModel batchModel = new BatchModel
            {
                Id = batch.Id,
                Name = batch.Name,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate,
                CourseId = batch.CourseId,
                Status = batch.Status,
                FaultyId = FaultyId,
            };
            return batchModel;
        }

        public IEnumerable<BatchView> convertListBatchView(IEnumerable<ClassRoom> list)
        {
            List<BatchView> data = new List<BatchView>();
            foreach (var room in list)
            {
                data.Add(convertBatchView(room));
            }
            return data;
        }

        public BatchView convertBatchView(ClassRoom room)
        {
            Faulty faulty = room.ClassRoomFaulties.Where(x => x.Status).First().Faulty;
            List<ClassRoomFaulty> List = room.ClassRoomFaulties.Where(x => x.Status == false).ToList();
            string faultyOld = "";
            for (int i = 0; i < List.Count(); i++)
            {
                if (i == List.Count() - 1)
                {
                    faultyOld += List[i].Faulty.Name;
                }
                else
                {
                    faultyOld += List[i].Faulty.Name + ',';
                }
            }
            int FaultyId = faulty.Id;
            string Status = "";
            if (room.Status == 1)
            {
                Status = "new";
            }
            else if (room.Status == 2)
            {
                Status = "In process";
            }
            else
            {
                Status = "It's over";
            }
            BatchView view = new BatchView
            {
                Id = room.Id,
                Code = room.Code,
                Name = room.Name,
                Course = room.Course.Name,
                Faulty = faulty.Name,
                User = room.User.FullName,
                FaultyOld = faultyOld,
                Status = Status,
                CreatedAt = room.CreatedAt,
                EndDate = room.EndDate,
                StartDate = room.StartDate,
                UpdatedAt = room.UpdatedAt
            };
            return view;
        }

        public ClassRoom convertEditBatch(BatchModel model, ClassRoom batch)
        {
            Course course = courseRepository.FindById(model.CourseId);
            batch.Name = model.Name;
            batch.StartDate = model.StartDate;
            batch.EndDate = GetDateTime(model.StartDate, course.StudyTime);
            batch.Status = model.Status;
            batch.CourseId = model.CourseId;
            batch.UpdatedAt = DateTime.Today;
            return batch;
        }

        private string GetCode()
        {
            Random random = new Random();
            int number =  random.Next(1000, 9999);
            int length = 1;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            string FirstName = str_build.ToString();
            return FirstName + number;
        }
        private DateTime GetDateTime(DateTime date, int StudyTime)
        {
            int NewDay = date.Day;
            int newYear = date.Year;
            int newMonth = date.Month;
            var month = date.Month + StudyTime;
            int[] month30 = {4,6,9,11};
            if (month > 12)
            {
                int year = month / 12;
                int mon = month % 12;
                if (mon == 0)
                {
                    newMonth = 12;
                }
                else
                {
                    newMonth = mon;
                }
                newYear = year + newYear;
            }
            else
            {
                newMonth = month;
            }
            if (newMonth == 2)
            {
                if (newYear % 4 == 0 && NewDay > 29)
                {
                    newMonth = 3;
                    NewDay = NewDay - 29;
                }
                else if (newYear % 4 != 0 && NewDay > 28)
                {
                    newMonth = 3;
                    NewDay = NewDay - 28;
                }
            }
            else if (Array.Exists(month30, element => element == newMonth) && NewDay > 30)
            {
                newMonth = newMonth + 1;
                NewDay = NewDay - 30;
            }
            DateTime endDate = new DateTime(newYear, newMonth, NewDay);
            return endDate;
        }
    }
}