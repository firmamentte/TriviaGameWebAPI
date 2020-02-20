/*
using MyGeneration/Template/NHibernate (c) by Firmament
*/
using NHibernate;
using NHibernate.Cfg;
using System.Threading.Tasks;
using TriviaGameWebAPI.Core;

namespace TriviaGameWebAPI.Data
{
    /// <summary>
    /// Handles creation and management of sessions and transactions.  It is a singleton because 
    /// building the initial session factory is very expensive. Inspiration for this class came 
    /// from Chapter 8 of Hibernate in Action by Bauer and King.  Although it is a sealed singleton
    /// you can use TypeMock (http://www.typemock.com) for more flexible testing.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        
        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSessionManager Instance {
            get {
                return Nested.NHibernateSessionManager;
            }
        }

        /// <summary>
        /// Initializes the NHibernate session factory upon instantiation.
        /// </summary>
        private ISessionFactory SessionFactory { get; set; }
        private ISession Session { get; set; }
		private ITransaction Transaction { get; set; }
        private NHibernateSessionManager() {
            InitialiseSessionFactory();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateSessionManager NHibernateSessionManager = new NHibernateSessionManager();
        }

        #endregion

        private void InitialiseSessionFactory() {
            Configuration _configuration = new Configuration();
            _configuration.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            _configuration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2012Dialect");
            _configuration.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver");
            _configuration.SetProperty(Environment.ConnectionString, FirmamentUtilities.DatabaseHelper.ConnectionString);
            _configuration.SetProperty("use_proxy_validator", "true");
            _configuration.AddAssembly("TriviaGameWebAPI.Core");
            SessionFactory = _configuration.BuildSessionFactory();
        }

        public ISession GetSession() {
            return OpenSession();
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession OpenSession() {
            if (Session == null) {
                    Session = SessionFactory.OpenSession();
            }
            return Session;
        }

        /// <summary>
        /// Flushes anything left in the session and closes the connection.
        /// </summary>
        private void CloseSession() {
            if (Session != null && Session.IsOpen) {
                Session.Close();
            }
            Session = null;
        }

        public void BeginTransaction() {
            if (Transaction == null) {
                Transaction = GetSession().BeginTransaction();
            }
        }

        public async Task CommitTransaction() {
            try {
                if (HasOpenTransaction()) {
                   await Transaction.CommitAsync();
                }
                Transaction = null;
            }
            catch (HibernateException) {
                await RollbackTransaction();
                throw;
            }
        }

        private bool HasOpenTransaction() {
            return Transaction != null && !Transaction.WasCommitted && !Transaction.WasRolledBack;
        }

        public async Task RollbackTransaction() {
            try {
                if (HasOpenTransaction()) {
                    await Transaction.RollbackAsync();
                }
                Transaction = null;
            }
            finally {
                CloseSession();
            }
        }
    }
}
