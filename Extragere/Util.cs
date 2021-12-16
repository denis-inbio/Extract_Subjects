using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extragere
{
    class Util
    {
        public static bool isSeparator_Id_Title(char symbol_ASCII)
        {
            return symbol_ASCII == '.' || symbol_ASCII == '-';
        }

        public static bool isSpace(char symbol_ASCII)
        {
            return symbol_ASCII == ' ' || symbol_ASCII == '\t';
        }

        public static bool isDecimal(char symbol_ASCII) {
            return '0' <= symbol_ASCII && symbol_ASCII <= '9';
        }

        public static bool isText_English(char symbol_ASCII)
        {
            return (' ' <= symbol_ASCII && symbol_ASCII <= '~');
        }

        public static bool isText_Romanian(char symbol_ASCII)
        {
            return  (' ' <= symbol_ASCII && symbol_ASCII <= '~') ||
                    symbol_ASCII == 'ă' || symbol_ASCII == 'Ă' ||
                    symbol_ASCII == 'â' || symbol_ASCII == 'Â' ||
                    symbol_ASCII == 'î' || symbol_ASCII == 'Î' ||
                    symbol_ASCII == 'ș' || symbol_ASCII == 'Ș' ||
                    symbol_ASCII == 'ț' || symbol_ASCII == 'Ț';
        }

        public static bool isName_Romanian(char symbol_ASCII)
        {
            // (*): optimizations such as "upper case vs lower case" being checked using one bit check
            // are only relevant depending on the encoding (!); the ordering of the space takes precedence
            // for naive, (!) but robust solutions
            return  ('a' <= symbol_ASCII && symbol_ASCII <= 'z') ||
                    ('A' <= symbol_ASCII && symbol_ASCII <= 'Z') ||
                    symbol_ASCII == 'ă' || symbol_ASCII == 'Ă' ||
                    symbol_ASCII == 'â' || symbol_ASCII == 'Â' ||
                    symbol_ASCII == 'î' || symbol_ASCII == 'Î' ||
                    symbol_ASCII == 'ș' || symbol_ASCII == 'Ș' ||
                    symbol_ASCII == 'ț' || symbol_ASCII == 'Ț';
        }

        public static bool isLineDelimiter(char symbol_ASCII) {
            return symbol_ASCII == '\n';
        }
    }
}
