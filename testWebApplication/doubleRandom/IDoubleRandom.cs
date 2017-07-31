using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.doubleRandom
{
    public interface IDoubleRandom : IPollutionSourceToRandom, ICantonBindDepart, IDoubleRandomRecord
    {



    }

    public interface IPollutionSourceToRandom
    {
        object pagePollutionSourceToRandom(object paramterData);

        object listPollutionSourceToRandomByUserId(object paramterData);

        bool savePollutionSourceToRandom(object entityList);

        bool deletePollutionSourceToRandom(object idList);
    }

    public interface ICantonBindDepart
    {
        object pageCantonBindDepart(object paramterData);

        bool saveCantonBindDepart(object entityList);

        bool deleteCantonBindDepart(object idList);
    }

    public interface IDoubleRandomOperate
    {
        bool saveDoubleRandomUser(object pollutionSourceList, object userList);

        bool saveDoubleRandomDepart(object pollutionSourceList, object departList);

        bool updateDoubleRandomState(object doubleRandomList);
    }

    public interface IDoubleRandomRecord
    {
        object pageDoubleRandomRecord(object paramterData);

        object pageDoubleRandomPollutionSourceRecord(object paramterData);
    }
}