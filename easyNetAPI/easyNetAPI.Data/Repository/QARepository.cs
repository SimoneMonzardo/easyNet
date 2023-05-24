using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class QARepository
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public QARepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //da correggere
        //public void Update(QA qa)
        //{
        //    var QAfromDb = GetFirstOrDefault(q => q.Intent == qa.Intent);
        //    if (QAfromDb is not null)
        //    {
        //        QAfromDb.Answer = qa.Answer;
        //        QAfromDb.Questions = qa.Questions;
        //    }
        //}
    }
}