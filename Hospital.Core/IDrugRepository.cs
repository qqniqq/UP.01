using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core
{
    public interface IDrugRepository
    {
        // Создать медикамент. Возвращает true при успехе, false при ошибке.
        bool CreateDrug(Drug drug, out int createdId);

        Drug GetDrugById(int id);
    }
}
