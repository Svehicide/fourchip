using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fourchip
{
    class Rs232_string
    {
        public const String HELLO = "#00@";                     //#00@
        public const String SCAN = "#01@";                      //#01@Firstname;Name
        public const String LOGIN = "#02@";                     //#02@1 ( OK ) ou #02@0 ( NOT OK ) ou #02@2 ( TIME OUT )
        
        public const String CARD_IP = "#31@";                   //#31@x.x.x.x
        public const String CARD_NETWORK = "#32@";              //#32@x.x.x.x
        public const String CARD_GW = "#33@";                   //#33@x.x.x.x
        public const String CARD_DNS1 = "#34@";                 //#34@x.x.x.x
        public const String CARD_DNS2 = "#35@";                 //#35@x.x.x.x

        public const String TEMP = "#04@";                      //#04@tempValue
        
        public const String LIGHT = "#05@";                     //#05@lightValue
        
        public const String LED = "#06@";                       //#06@ledFreq
    }
}