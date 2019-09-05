using NUnit.Framework;
using DerivcoTestTask.Models;
using DerivcoTestTask.Services;

namespace DerivcoTestTask.Tests
{
    public class Tests
    {
        [Test]
        public void IsAllowed_Map1x2_False()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,] { { "#", "#" } }
            };

            Assert.AreEqual("The Operation can not be completed. The size of the map should be 2x2 and bigger.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_Empty()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "#", "#", "#" ,"#"},
                    { "#", "#", "#" ,"#"},
                    { "#", "#", "#" ,"#"},
                    { "#", "#", "#", "#" }
                }
            };

            Assert.AreEqual("The given map does not have surface area of water.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_3()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "#", "#", "O", "#"},
                    { "#", "#", "O", "O" },
                    { "#", "#", "#" ,"#"},
                    { "#", "#", "#", "#" }
                }
            };

            Assert.AreEqual("The given map has surface area of 3 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_1_1_1_1()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "#", "#", "O", "#"},
                    { "#", "#", "#", "O" },
                    { "O", "#", "#" ,"#"},
                    { "#", "O", "#", "#" }
                }
            };

            Assert.AreEqual("The given map has surface area of 1, 1, 1, 1 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_3_3()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "#", "#", "O", "#"},
                    { "#", "#", "O", "O" },
                    { "O", "O", "#" ,"#"},
                    { "#", "O", "#", "#" }
                }
            };

            Assert.AreEqual("The given map has surface area of 3, 3 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_Map2x2_1_1()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "O", "#" },
                    { "#", "O" }
                }
            };

            Assert.AreEqual("The given map has surface area of 1, 1 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_Map2x2_3()
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "O", "O" },
                    { "#", "O" }
                }
            };

            Assert.AreEqual("The given map has surface area of 3 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }

        [Test]
        public void SurfaceAreaOfWater_Map10x10_8_1_1_4()   
        {
            var areaSurface = new AreaSurfaceCalculationService();

            var model = new CalculatingSurfaceIncomeModel
            {
                Coordinates = new[] { 0, 0 },
                Map = new[,]
                {
                    { "O", "#", "#", "#", "#", "#", "#", "#", "#", "#"},
                    { "O", "O", "#", "#", "#", "#", "#", "#", "#", "#"},
                    { "#", "O", "O", "O", "#", "#", "#", "#", "#", "#"},
                    { "#", "O", "O", "#", "#", "#", "#", "#", "#", "#"},
                    { "#", "#", "#", "#", "#", "#", "O", "#", "#", "#"},
                    { "#", "#", "#", "#", "#", "#", "#", "O", "#", "#"},
                    { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#"},
                    { "O", "#", "#", "#", "#", "#", "#", "#", "#", "#"},
                    { "O", "#", "#", "#", "#", "#", "#", "#", "#", "#"},
                    { "O", "O", "#", "#", "#", "#", "#", "#", "#", "#"}
                }
            };

            Assert.AreEqual("The given map has surface area of 8, 1, 1, 4 square meters.", areaSurface.CalculateSurfaceAreas(model));
        }
    }
}