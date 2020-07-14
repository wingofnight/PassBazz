using System;
using System.IO;

namespace PasswordCheck
{
    
    public class Password
    {
      

        private readonly int length;

        private readonly string symbols1;
        private readonly string symbols2;
        private readonly string symbols3;
        private readonly string symbols4;
        
        private readonly string symbols;

        public Password()
        {
        
            using (StreamReader file = new StreamReader("password.ini"))
            {
                string tempLine;
                while ((tempLine = file.ReadLine()) != null)
                {
                    int tempIndex = tempLine.IndexOf('=');
                    if (tempIndex == -1) continue;
                    string tempVar = tempLine.Substring(0, tempIndex);
                    string tempSymbols = tempLine.Substring(tempIndex + 1);
                    switch (tempVar)
                    {
                        case "lenght":
                            length = Convert.ToInt32(tempSymbols);
                            break;
                        case "symbols1":
                            symbols1 = tempSymbols;
                            break;
                        case "symbols2":
                            symbols2 = tempSymbols;
                            break;
                        case "symbols3":
                            symbols3 = tempSymbols;
                            break;
                        case "symbols4":
                            symbols4 = tempSymbols;
                            break;

                    }
                }
            }


            symbols = symbols1 + symbols2 + symbols3 + symbols4;
        }

        public (bool acces, string error) MinLength(string password)
        {
            string Er;
            if (password.Length < 8)
            {
                Er = $"Пароль содержит меньше {8} символов";
                return (false,Er);
            }
            else
            {
                Er=("Пароль имеет достаточное количество символов");
                return (true,Er);
            }
        }

        public (bool acces, string error) CheckSymbols(string password)
        {
            string Er;
            int pass_check1 = password.IndexOfAny(symbols1.ToCharArray());
            int pass_check2 = password.IndexOfAny(symbols2.ToCharArray());
            int pass_check3 = password.IndexOfAny(symbols3.ToCharArray());
            int pass_check4 = password.IndexOfAny(symbols4.ToCharArray());

            if (pass_check1 == -1 || pass_check2 == -1 || pass_check3 == -1 || pass_check4 == -1)
            {
                Er = ("Pass need Aa1234!@ (8 symbol, punctuation, uppercase, lowercase)!");
                return (false,Er);
            }
            else
            {
                Er = ("Пароль соответстует минимальным требованиям");
                return (true,Er);
            }
        }

        public (bool acces, string error) CheckAlphabet(string password)
        {
            string Er;
            foreach(var symbol in password)
            {
                if (!symbols.Contains(symbol))
                {
                    Er=("Пароль содержит зпрещённые символы");
                    return (false,Er);
                }
            }
            Er=("Пароль не содержит запрещённые символы");
            return (true,Er);
        }
        public (bool accept, string error) CheckPass(string password)
        {
            var result = (false,"Unknown Error");
           
            
            if (MinLength(password).acces && CheckSymbols(password).acces && CheckAlphabet(password).acces)
            {
                result = (true, "Acces complite");
            }
            
            return result;
        }
    }
}
