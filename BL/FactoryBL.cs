using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBL
    {
        private static IBL ibl = null;
        public static IBL GetBL()
        {

            if (ibl == null)
                ibl = new BLImp();
            return ibl;

        }
    }
}
