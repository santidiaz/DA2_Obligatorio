using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyPersistance
{
    public class SystemDummyData
    {
        private List<Event> events;
        private List<Sport> sports;
        private List<Team> teams;
        private List<User> users;

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<SystemDummyData> instance = new Lazy<SystemDummyData>(() => new SystemDummyData());
        private SystemDummyData()
        {
            this.events = new List<Event>();
            this.teams = new List<Team>();
            this.sports = new List<Sport>();
            this.users = new List<User>();
        }
        public static SystemDummyData GetInstance
        {
            get
            {
                return instance.Value;
            }
        }
        #endregion

        public void Reset()
        {
            this.events.Clear();
            this.teams.Clear();
            this.sports.Clear();
            this.users.Clear();
        }

        public List<Event> GetEvents()
        {
            return this.events;
        }
        public List<Team> GetTeams()
        {
            return this.teams;
        }
        public List<Sport> GetSports()
        {
            return this.sports;
        }

        public List<User> GetUsers()
        {
            return this.users;
        }
    }
}
