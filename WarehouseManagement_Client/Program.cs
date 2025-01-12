using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Linq;
using ServiceReference1;

namespace WarehouseManagement_Client
{
    public class WhsClient
    {
        public static async Task Main(string[] args)
        {
            using (WareHouseClient ClientCls = new WareHouseClient())
            {
                try
                {
                    PrimaryOption:
                        Console.WriteLine("===============Enter your option================");
                        Console.WriteLine("A for Insert Method");
                        Console.WriteLine("B for Fetch Method");
                        Console.WriteLine("C for Specific Method");
                        string? option = Console.ReadLine();

                    switch (option?.ToLower())
                    {
                        case "a":
                            Console.WriteLine("==========Enter data of items=========");

                            List<dynamic> InsertItemDataList = new List<dynamic>();

                            InsertData:
                                Console.WriteLine("Enter Item Id:- ");
                                string? ItemId = Console.ReadLine();
                                Console.WriteLine("Enter Item Name:- ");
                                string? ItemName = Console.ReadLine();
                                Console.WriteLine("Enter Category:- ");
                                string? Category = Console.ReadLine();
                                Console.WriteLine("Enter Item Type:- ");
                                string? ItemType = Console.ReadLine();
                                Console.WriteLine("Enter Warehouse Name:- ");
                                string? WhsName = Console.ReadLine();
                                Console.WriteLine("Enter Item Price:- ");
                                double Price = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter Item Quantity:- ");
                                int Qty = Convert.ToInt32(Console.ReadLine());

                                var Insert = new
                                {
                                    Id = ItemId,
                                    Name = ItemName,
                                    Cat = Category,
                                    Type = ItemType,
                                    Warehouse = WhsName,
                                    IPrice = Price,
                                    IQty = Qty
                                };
                                InsertItemDataList.Add(Insert);

                            Console.WriteLine("Insert more item details? (y/n)");
                            string? continueInput = Console.ReadLine()?.Trim();

                            if(continueInput?.ToLower() == "y")
                            {
                                goto InsertData;
                            }
                            else if(continueInput?.ToLower() == "n")
                            {
                                Console.WriteLine("To Insert Data, Press I key:");
                                string? InsOp = Console.ReadLine()?.Trim();

                                if(InsOp?.ToLower() == "i")
                                {
                                    Console.WriteLine(" ===============Output Response================ ");
                                    foreach (var item in InsertItemDataList)
                                    {
                                        string? response = await ClientCls.InsertItemDataAsync(
                                                                item.Id,
                                                                item.Name,
                                                                item.Cat,
                                                                item.Type,
                                                                item.Warehouse,
                                                                item.IPrice,
                                                                item.IQty
                                                            );

                                        Console.WriteLine(response);
                                    }

                                    goto PrimaryOption;
                                }
                            }

                            break;

                        case "b":
                            Console.WriteLine("==================Display All Data=================");
                            var fetchData = await ClientCls.GetAllItemDataAsync();

                            if(fetchData != null && fetchData.Length > 0)
                            {
                                if (fetchData.Length == 1 && !string.IsNullOrEmpty(fetchData[0].Message))
                                {
                                    Console.WriteLine(fetchData[0].Message);
                                    goto PrimaryOption;
                                }
                                else if(fetchData.Length == 1 && !string.IsNullOrEmpty(fetchData[0].Err))
                                {
                                    Console.WriteLine(fetchData[0].Err);
                                    goto PrimaryOption;
                                }
                                else
                                {
                                    foreach(var item in fetchData)
                                    {
                                        Console.WriteLine($"Database Row Id:- {item.RowId}");
                                        Console.WriteLine($"{item.ItemId}");
                                        Console.WriteLine($"{item.ItemName}");
                                        Console.WriteLine($"{item.ItemCategory}");
                                        Console.WriteLine($"{item.ItemPrice}");
                                        Console.WriteLine($"{item.ItemQty}");
                                        Console.WriteLine($"{item.ItemType}");
                                        Console.WriteLine($"{item.WhName}");
                                        Console.WriteLine(" ");
                                    }
                                    goto PrimaryOption;
                                }
                            }
                            
                            break;

                        case "c":
                            Console.WriteLine("==================Display Specific Data=================");
                            Console.WriteLine("Enter Item Id:- ");
                            string? InputItemId = Console.ReadLine();

                            var fetchDataSpecific = await ClientCls.GetSpecificItemDataAsync(InputItemId);

                            if (fetchDataSpecific != null && fetchDataSpecific.Length > 0)
                            {
                                if (fetchDataSpecific.Length == 1 && !string.IsNullOrEmpty(fetchDataSpecific[0].Message))
                                {
                                    Console.WriteLine(fetchDataSpecific[0].Message);
                                    goto PrimaryOption;
                                }
                                else if (fetchDataSpecific.Length == 1 && !string.IsNullOrEmpty(fetchDataSpecific[0].Err))
                                {
                                    Console.WriteLine(fetchDataSpecific[0].Err);
                                    goto PrimaryOption;
                                }
                                else
                                {
                                    foreach (var item in fetchDataSpecific)
                                    {
                                        Console.WriteLine($"Database Row Id:- {item.RowId}");
                                        Console.WriteLine($"{item.ItemId}");
                                        Console.WriteLine($"{item.ItemName}");
                                        Console.WriteLine($"{item.ItemCategory}");
                                        Console.WriteLine($"{item.ItemPrice}");
                                        Console.WriteLine($"{item.ItemQty}");
                                        Console.WriteLine($"{item.ItemType}");
                                        Console.WriteLine($"{item.WhName}");
                                        Console.WriteLine(" ");
                                    }
                                    goto PrimaryOption;
                                }
                            }
                            break;

                         default:
                            Console.WriteLine("Please choose proper option.");
                            goto PrimaryOption;
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("================Exception Message==============");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}