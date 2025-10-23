using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core
{
    public class DrugService
    {
        private readonly IDrugRepository _repository;

        public DrugService(IDrugRepository repository)
        {
            _repository = repository;
        }

        // Возвращает пустую строку при успехе, иначе текст ошибки
        public string AddDrug(Drug drug)
        {
            if (drug == null) return "Данные препарата отсутствуют.";
            if (string.IsNullOrWhiteSpace(drug.Name)) return "Наименование препарата не может быть пустым.";
            if (drug.ExpirationDate <= DateTime.Now.Date) return "Срок годности препарата истёк.";

            bool ok = _repository.CreateDrug(drug, out int createdId);
            if (!ok) return "Ошибка при сохранении препарата в БД.";

            return string.Empty;
        }
    }
}
