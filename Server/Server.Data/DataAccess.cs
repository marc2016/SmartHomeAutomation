using System;
using System.IO;
using System.Linq;

using Windows.Storage;

using Server.Data.Models;

using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace Server.Data
{
    public class DataAccess
    {
        #region Fields

        private static volatile DataAccess _instance;
        private static readonly object _syncRoot = new object();
        private readonly SQLiteConnection dataConnection;

        #endregion

        #region Constructor

        private DataAccess()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            this.dataConnection = new SQLiteConnection(new SQLitePlatformWinRT(), path);
            this.dataConnection.CreateTable<Switch>();
        }

        #endregion

        #region Properties

        public static DataAccess Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new DataAccess();
                    }

                return _instance;
            }
        }

        #endregion

        #region Method

        public void AddOrUpdate(ModelBase model)
        {
            if (model.Id > 0)
                this.dataConnection.Update(model);
            else
                this.dataConnection.Insert(model);
        }

        #endregion
    }
}