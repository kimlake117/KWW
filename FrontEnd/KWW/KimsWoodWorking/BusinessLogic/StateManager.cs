using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using Dapper;

namespace KimsWoodWorking.BusinessLogic
{
    public static class StateManager
    {
        public static List<StateModelDB> getStates() { 
            
            var p = new DynamicParameters();

            string sql = "select * from state";

            List<StateModelDB> states = SqliteDataAccess.LoadData<StateModelDB>(sql,p);

            return states;
        }
    }
}