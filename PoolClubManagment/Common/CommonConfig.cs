using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class CommonConfig
    {
        #region SingleTone

        private static CommonConfig m_instance;

        public static CommonConfig GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new CommonConfig();
            }

            return m_instance;
        }

        #endregion

        #region Data Members

        private Dictionary<int, double> m_dicPricePerMin; 

        #endregion

        #region Properties

        public Dictionary<int, double> PricePerMin
        {
            get { return m_dicPricePerMin; }
        }

        #endregion

        #region Ctor

        private CommonConfig()
        {
            m_dicPricePerMin = new Dictionary<int, double>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the dictionary of price per minutes game
        /// </summary>
        /// <param name="dicPricePerMin">The dictionary</param>
        public void SetPricePerMin(Dictionary<int, double> dicPricePerMin)
        {
            m_dicPricePerMin = dicPricePerMin;
        }

        #endregion
    }
}
