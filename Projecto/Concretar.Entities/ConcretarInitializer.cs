using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concretar.Entities
{
    public class ConcretarInitializer //: IDatabaseInitializer<ConcretarContext>
    {
        public bool nueva=false;
        public void InitializeDatabase(ConcretarContext context)
        {
            bool dbExists;
            dbExists = context.Database.EnsureCreated();
            /*if (dbExists)
            {
                try
                {
                    if (!context.Database.CompatibleWithModel(true))
                    {
                        throw new Exception("La base de datos existe y no es compatible...");
                    }
                }
                catch
                {
                    return;
                }
            }
            else
            {
                context.Database.Create();
                context.SaveChanges();
                nueva = true;
                return;
            }*/
            return;
        }

       
        public void CreateUser(ConcretarContext context)
        {
            
        }

        protected void Seed(ConcretarContext context)
        
        {

        }
    }
}