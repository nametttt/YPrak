using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPrak
{
    class Fabric
    {
        public int Width { get; set; } // ширина ткани
        public int Length { get; set; } // длина ткани
        public List<Cut> Cuts { get; set; } // список резов на ткани
    }

    class Cut
    {
        public int Width { get; set; } // ширина реза
        public int Length { get; set; } // длина реза
    }

    class Tovar
    {
        public string Name { get; set; } // название изделия
        public int Width { get; set; } // ширина изделия
        public int Length { get; set; } // длина изделия
    }

    class Cutting
    {
        public bool CutFabric(Fabric fabric, List<Tovar> products)
        {
            // сортировка изделий по площади по убыванию
            products = products.OrderByDescending(p => p.Width * p.Length).ToList();

            // проход по каждому изделию в списке
            foreach (var product in products)
            {
                // поиск наиболее подходящего реза на ткани
                Cut bestCut = null;
                int bestArea = 0;
                foreach (var cut in fabric.Cuts)
                {
                    if ((cut.Width >= product.Width && cut.Length >= product.Length) ||
                        (cut.Length >= product.Width && cut.Width >= product.Length))
                    {
                        // рассчитываем площадь оставшейся ткани
                        int remainingArea = (cut.Width - product.Width) * cut.Length +
                                            (cut.Length - product.Length) * cut.Width;

                        // сохраняем рез с наибольшей площадью оставшейся ткани
                        if (remainingArea > bestArea)
                        {
                            bestArea = remainingArea;
                            bestCut = cut;
                        }
                    }
                }

                // если найден подходящий рез, то добавляем изделие на ткань и сохраняем рез
                if (bestCut != null)
                {
                    int remainingWidth = bestCut.Width - product.Width;
                    int remainingLength = bestCut.Length - product.Length;

                    if (remainingWidth > 0)
                    {
                        fabric.Cuts.Add(new Cut { Width = remainingWidth, Length = bestCut.Length });
                    }

                    if (remainingLength > 0)
                    {
                        fabric.Cuts.Add(new Cut { Width = bestCut.Width, Length = remainingLength });
                    }

                    fabric.Cuts.Remove(bestCut);
                    fabric.Cuts.Add(new Cut { Width = product.Width, Length = product.Length });
                }
                else
                {
                    // если не удалось найти подходящий рез, то возвращаем false
                    return false;
                }
            }

            // если все изделия были раскроены успешно, то возвращаем true
            return true;
        }
    }
}