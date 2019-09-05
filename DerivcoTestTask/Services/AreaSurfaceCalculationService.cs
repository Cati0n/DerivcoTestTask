using System.Collections.Generic;
using System.Linq;
using DerivcoTestTask.Models;

namespace DerivcoTestTask.Services
{
    public class AreaSurfaceCalculationService : ISurfaceAreasInterface
    {
        public string CalculateSurfaceAreas(CalculatingSurfaceIncomeModel model)
        {
            var border = new[] { model.Map.GetLength(0) - 1, model.Map.GetLength(1) - 1 };
            var ignoreList = new List<int[]>();
            var waterChunks = new List<int>();

            if (model.Map.GetLength(0) < 2 || model.Map.GetLength(1) < 2)
                return "The Operation can not be completed. The size of the map should be 2x2 and bigger.";

            for (var y = model.Coordinates[0]; y <= model.Map.GetLength(0) - 1; y++)
            {
                for (var x = model.Coordinates[1]; x <= model.Map.GetLength(1) - 1; x++)
                {
                    if (model.Map[y, x].Equals("O"))
                    {
                        var checker = FindElementsInListOfArrays(new[] { y, x }, ignoreList);

                        if (!checker)
                            ignoreList.Add(new[] { y, x });

                        if (y + 1 <= border[0])
                            if (model.Map[y + 1, x].Equals("O"))
                            {
                                checker = FindElementsInListOfArrays(new[] { y + 1, x }, ignoreList);
                                if (!checker)
                                    ignoreList.Add(new[] { y + 1, x });
                            }

                        if (x + 1 <= border[1])
                            if (model.Map[y, x + 1].Equals("O"))
                            {
                                checker = FindElementsInListOfArrays(new[] { y, x + 1 }, ignoreList);
                                if (!checker)
                                    ignoreList.Add(new[] { y, x + 1 });
                            }


                        if (x == border[1] && ignoreList.Count != 0 && model.Map[y, x].Equals("O"))
                        {
                            var isThereWaterBehind = CheckCoordsBehind(new[] { y, x }, ignoreList, border[1]);
                            if (!isThereWaterBehind)
                                ignoreList = SortListByMakingUniqueItemsOnly(ignoreList, waterChunks);
                        }
                    }

                    else if (y == border[0] && x == border[1])
                        ignoreList = SortListByMakingUniqueItemsOnly(ignoreList, waterChunks);


                    else if (x == border[1] && ignoreList.Count != 0)
                    {
                        var isThereWaterBehind = CheckCoordsBehind(new[] { y, x }, ignoreList, border[1]);
                        if (!isThereWaterBehind)
                            ignoreList = SortListByMakingUniqueItemsOnly(ignoreList, waterChunks);
                    }
                }
            }

            string providedInformation;
            if (waterChunks.Count == 0 || waterChunks[0] == 0)
                providedInformation = "The given map does not have surface area of water.";
            else
                providedInformation = "The given map has surface area of " + string.Join(", ", waterChunks) + " square meters.";

            return providedInformation;
        }

        private bool CheckCoordsBehind(int[] coords, List<int[]> listOfCoords, int border)
        {
            for (int i = 0; i <= border; i++)
            {
                foreach (var element in listOfCoords.ToList())
                {
                    if (element[0] == coords[0])
                    {
                        if (element[1].Equals(coords[1] - i))
                            if (listOfCoords.Any(c => c[0] == element[0] + 1) &&
                                    listOfCoords.Any(c => c[0] == element[0] - 1) ||
                                    listOfCoords.Any(c => c[1] == element[1] - 1) &&
                                    listOfCoords.Any(c => c[0] == element[0] + 1))
                                return true;

                            else if (listOfCoords.Any(c => c[0] == element[0] - 1) ||
                                     listOfCoords.Any(c => c[1] == element[1] - 1) &&
                                     listOfCoords.Any(c => c[0] != element[0] + 1))
                                return false;

                            else if (listOfCoords.Count <= 3 && listOfCoords.Any(c => c[0] == element[0] + 1))
                                return true;
                    }
                }
            }

            return false;
        }

        private List<int[]> SortListByMakingUniqueItemsOnly(List<int[]> ignoredList, List<int> waterChunks)
        {
            foreach (var item in ignoredList.ToList())
            {
                var counter = 0;

                for (var i = 0; i < ignoredList.Count; i++)
                {
                    if (ignoredList[i].GetValue(0) == item.GetValue(0))
                    {
                        if (ignoredList[i].GetValue(1) == item.GetValue(1))
                        {
                            counter++;
                            if (counter >= 2)
                            {
                                ignoredList.RemoveAt(i);
                                counter = 1;
                            }
                        }
                    }
                }
            }
            if (ignoredList.Count != 0)
                waterChunks.Add(ignoredList.Count);

            ignoredList.Clear();

            return ignoredList;
        }

        private bool FindElementsInListOfArrays(int[] coords, List<int[]> listOfCoords)
        {
            foreach (var waterCoord in listOfCoords)
            {
                if (waterCoord[0] == coords[0] && waterCoord[1] == coords[1])
                    return true;
            }

            return false;
        }
    }
}
