using System;

namespace ProviderManager
{
    public class Provider
    {
        //private IUserLogic UserLogic;

        #region Singleton
        // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado.
        private static readonly Lazy<Provider> currentInstance = new Lazy<Provider>(() => new Provider());
        private Provider()
        {
            //this.UserLogic = new UserLogic(new UserPersistance());
        }
        public static Provider GetInstance
        {
            get
            {
                return currentInstance.Value;
            }
        }
        #endregion
    }
}
