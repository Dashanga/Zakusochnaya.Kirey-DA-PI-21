using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class PokupatelServiceList : IPokupatelService
    {
        private DataListSingleton source;
        public PokupatelServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PokupatelViewModel> GetList()
        {
            List<PokupatelViewModel> result = new List<PokupatelViewModel>();
            for (int i = 0; i < source.Pokupatels.Count; ++i)
            {
                result.Add(new PokupatelViewModel
                {
                    PokupatelId = source.Pokupatels[i].PokupatelId,
                    PokupatelFIO = source.Pokupatels[i].PokupatelFIO
                });
            }
            return result;
        }
        public PokupatelViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Pokupatels.Count; ++i)
            {
                if (source.Pokupatels[i].PokupatelId == id)
                {
                    return new PokupatelViewModel
                    {
                        PokupatelId = source.Pokupatels[i].PokupatelId,
                        PokupatelFIO = source.Pokupatels[i].PokupatelFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PokupatelBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Pokupatels.Count; ++i)
            {
                if (source.Pokupatels[i].PokupatelId > maxId)
                {
                    maxId = source.Pokupatels[i].PokupatelId;
                }
                if (source.Pokupatels[i].PokupatelFIO == model.PokupatelFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Pokupatels.Add(new Pokupatel
            {
                PokupatelId = maxId + 1,
                PokupatelFIO = model.PokupatelFIO
            });
        }
        public void UpdElement(PokupatelBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Pokupatels.Count; ++i)
            {
                if (source.Pokupatels[i].PokupatelId == model.PokupatelId)
                {
                    index = i;
                }
                if (source.Pokupatels[i].PokupatelFIO == model.PokupatelFIO &&
                source.Pokupatels[i].PokupatelId != model.PokupatelId)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Pokupatels[index].PokupatelFIO = model.PokupatelFIO;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Pokupatels.Count; ++i)
            {
                if (source.Pokupatels[i].PokupatelId == id)
                {
                    source.Pokupatels.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}