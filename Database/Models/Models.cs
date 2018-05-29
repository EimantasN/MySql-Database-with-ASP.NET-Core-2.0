using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Database
{
    public class G4
    {
        [Display(Name = "Kategorijos pavadinimas")]
        public string CategoryName { get; set; }
        [Display(Name = "Kategorijos sukūrimo data")]
        public string CategoryCreated { get; set; }
        [Display(Name = "Vartotojo vardas")]
        public string UserName { get; set; }
        [Display(Name = "Skelbimo prekės pavadinimas")]
        public string ItemName { get; set; }
        [Display(Name = "Skelmimų skaičius kategorijoje")]
        public int ItemsCount { get; set; }
        [Display(Name = "Vidutinis prekių kiekis")]
        public double AVGItemsQuantity { get; set; }
        [Display(Name = "Vidutinė skelbimo kaina")]
        public double AVGItemPrice { get; set; }

        public List<Group> GetGroups(List<G4> data)
        {
            List<Group> rData = new List<Group>();
            var sublist = new List<SubItmes>();
            G4 first = new G4();
            foreach (G4 model in data)
            {
                
                if (model.CategoryName != "")
                {
                    if (rData.Count == 0 && sublist.Count == 0)
                    {
                        first = model;
                    }
                    else
                    {
                        double sum = sublist.Sum(x => x.AVGItemPrice) + first.AVGItemPrice;
                        double GrangAvg = sum / (sublist.Count + 1);
                        Group add = new Group
                        {
                            g4 = first,
                            avg = GrangAvg,
                            subitmes = sublist
                        };
                        rData.Add(add);

                        first = model;
                    }
                }
                else
                {
                    var sub = new SubItmes
                    {
                        UserName = model.UserName,
                        ItemName = model.ItemName,
                        ItemsCount = model.ItemsCount,
                        AVGItemsQuantity = model.AVGItemsQuantity,
                        AVGItemPrice = model.AVGItemPrice
                    };
                    sublist.Add(sub);
                }

            }
            return rData;
        }

        public class Group
        {
            public G4 g4 { get; set; }
            public double avg { get; set;}
            public List<SubItmes> subitmes { get; set; }
        }

        public class SubItmes
        {
            [Display(Name = "Vartotojo vardas")]
            public string UserName { get; set; }
            [Display(Name = "Skelbimo prekės pavadinimas")]
            public string ItemName { get; set; }
            [Display(Name = "Skelmimų skaičius kategorijoje")]
            public int ItemsCount { get; set; }
            [Display(Name = "Vidutinis prekių kiekis")]
            public double AVGItemsQuantity { get; set; }
            [Display(Name = "Vidutinė skelbimo kaina")]
            public double AVGItemPrice { get; set; }
        }


    }


    public class AA1
    {
        [Display(Name = "Kategorijos pavadinimas")]
        public string Name { get; set; }
        [Display(Name = "Sukūrimo data")]
        public string Created { get; set; }
        [Display(Name = "Prekių skaičius kategorijoje")]
        public int PrekiuSkaicius { get; set; }
        [Display(Name = "Vidutinis prekių kiekis kategorijoje")]
        public double VidutinisPrekiuKiekis { get; set; }
    }

    public class Index
    {
        public List<int> aaa { get; set; }
    }

    public class Categorys
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }

    public class Citys
    {
        [Display(Name = "Miesto pavadinimas")]
        public string Name { get; set; }
        public int id { get; set; }
    }

    public class Comments
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool Ban { get; set; }

        //Keys
        public int fk_User_Comments { get; set; }
        public int fk_Item_Comments { get; set; }
    }

    public class Conversations
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Created { get; set; }
        public DateTime Last_updated { get; set; }

        //Keys
        public int fk_Conversation_Users { get; set; }
        public int fk_User_Conversations { get; set; }
    }

    public class Countrys
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Keys
        public int fk_User_Conversations { get; set; }
    }

    public class Education
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ExpandedForms
    {
        public int Id { get; set; }
        public string Property { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //Keys
        public int fk_Category_Expanded_form { get; set; }
    }

    public class Favorites
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        //Keys
        public int fk_User_Favorites { get; set; }
        public int fk_Items_Favorites { get; set; }
    }

    public class Intrests
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        //Keys
        public int fk_User_Featured_offers { get; set; }
        public int fk_Intrest_Items { get; set; }
    }

    public class Historys
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        //Keys
        public int fk_User_Historys { get; set; }
        public int fk_History_Items { get; set; }
    }

    public class Images
    {
        [Display(Name = "Aukštos kokybės img adresas")]
        public string Image { get; set; }
        [Display(Name = "Žemos kokybes img adresas")]
        public string Image_thumbnail { get; set; }
        [Display(Name = "Indetifikavimo numeris")]
        public int id { get; set; }

        //Keys
        [Display(Name = "Indetifikavimo skelbime")]
        public int fk_Item_Images { get; set; }
    }

    public class ItemImage
    {
        [Display(Name = "Pavavinimas")]
        public string Name { get; set; }
        [Display(Name = "Kiekis")]
        public int Quantity { get; set; }
        [Display(Name = "Sukurta")]
        public string Created { get; set; }
        [Display(Name = "Atnaujinta")]
        public string Updated { get; set; }
        [Display(Name = "Aprašymas")]
        public string Description { get; set; }
        [Display(Name = "Pranešti apie susidomėjimus")]
        public bool Report_likes { get; set; }
        [Display(Name = "Pranešti apie komentarus")]
        public bool ReportAboutComment { get; set; }
        [Display(Name = "Pranešti apie pasiūlymus")]
        public bool Report_about_offer { get; set; }
        [Display(Name = "Kiek kartų apsilankyta")]
        public int Visited_times { get; set; }
        [Display(Name = "Kiek pasiūlymų sulaukė skelbimas")]
        public int Received_offers { get; set; }
        //Foreign Key Status
        [Display(Name = "Būsena")]
        public int Status { get; set; }
        [Display(Name = "Identifikacinis numeris")]
        public int Id { get; set; }

        //Keys
        [Display(Name = "Identifikacinis kategorijios numeris")]
        public int fk_Category_Items { get; set; }
        [Display(Name = "Identifikacinis vartotojo numeris")]
        public int fk_User_Items { get; set; }
        [Display(Name = "Aukštos kokybės img adresas")]
        public string Image { get; set; }
        [Display(Name = "Žemos kokybes img adresas")]
        public string Image_thumbnail { get; set; }
        [Display(Name = "Indetifikavimo numeris")]
        public int id { get; set; }

        //Keys
        [Display(Name = "Indetifikavimo skelbime")]
        public int fk_Item_Images { get; set; }
    }


    public class items
    {
        [Display(Name = "Pavavinimas")]
        public string Name { get; set; }
        [Display(Name = "Kiekis")]
        public int Quantity { get; set; }
        [Display(Name = "Sukurta")]
        public string Created { get; set; }
        [Display(Name = "Atnaujinta")]
        public string Updated { get; set; }
        [Display(Name = "Aprašymas")]
        public string Description { get; set; }
        [Display(Name = "Pranešti apie susidomėjimus")]
        public bool Report_likes { get; set; }
        [Display(Name = "Pranešti apie komentarus")]
        public bool ReportAboutComment { get; set; }
        [Display(Name = "Pranešti apie pasiūlymus")]
        public bool Report_about_offer { get; set; }
        [Display(Name = "Kiek kartų apsilankyta")]
        public int Visited_times { get; set; }
        [Display(Name = "Kiek pasiūlymų sulaukė skelbimas")]
        public int Received_offers { get; set; }
        //Foreign Key Status
        [Display(Name = "Būsena")]
        public int Status { get; set; }
        [Display(Name = "Identifikacinis numeris")]
        public int Id { get; set; }

        //Keys
        [Display(Name = "Identifikacinis kategorijios numeris")]
        public int fk_Category_Items { get; set; }
        [Display(Name = "Identifikacinis vartotojo numeris")]
        public int fk_User_Items { get; set; }
    }

    public class Messages
    {
        public int id { get; set; }
        public string Message { get; set; }
        public bool haveRead { get; set; }
        public bool badContent { get; set; }
        public DateTime Created { get; set; }

        //Keys
        public int fk_Conversations_Messages { get; set; }
    }

    public class SpecificForms
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Property { get; set; }
        public bool Approved { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //Keys
        public int fk_Item_Specific_form { get; set; }
    }

    public class Prices
    {
        [Display(Name = "Kaina")]
        public double Price { get; set; }
        [Display(Name = "Sukurta")]
        public string Created { get; set; }
        [Display(Name = "Identifikacinis numeris")]
        public int id { get; set; }


        //Keys
        [Display(Name = "Skelbimo identifikacinis numeris")]
        public int fk_Item_Prices { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Users
    {
        [Display(Name = "Vardas")]
        public string Name { get; set; }
        [Display(Name = "Pavardė")]
        public string LastName { get; set; }
        [Display(Name = "Slapyvardis")]
        public string UserName { get; set; }
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }
        [Display(Name = "Elektroninis pašto adresas")]
        public string Email { get; set; }
        [Display(Name = "Lytis")]
        public bool Gender { get; set; }
        [Display(Name = "Mėnesinės pajamos")]
        public int Income { get; set; }
        [Display(Name = "Telefono numeris")]
        public string Phone_nr { get; set; }
        [Display(Name = "Mobiliojo telefono numeris")]
        public string Mobile_nr { get; set; }
        [Display(Name = "Atstovaujate įmonei?")]
        public bool IsComany { get; set; }
        [Display(Name = "Įmonės pavadinimas")]
        public string Company_name { get; set; }
        [Display(Name = "Amžius")]
        public int Age { get; set; }
        [Display(Name = "Rodyti elektroninio pašto adresą")]
        public bool Show_email { get; set; }
        [Display(Name = "Rodyti telefono numerį")]
        public bool Show_phone_nr { get; set; }
        [Display(Name = "Rodyti mobiliojo telefono numerį")]
        public bool Show_mobile_nr { get; set; }
        [Display(Name = "Rodyti miestą kuriame esate")]
        public bool Show_location { get; set; }
        [Display(Name = "Rodyti tikslų namų adresą")]
        public bool Show_the_exact_address { get; set; }
        [Display(Name = "Leisti siųsti žinutes jums")]
        public bool Allow_send_me_messages { get; set; }
        [Display(Name = "Kaip dažnai informuoti (kartai per savaitę)")]
        public int How_often_inform { get; set; }
        [Display(Name = "Kelinta valanda informuoti (Pvz - 16:00 įvesti 16)")]
        public int Which_time_inform { get; set; }
        [Display(Name = "Siųsti pranešimus apie favorininius skelbimus")]
        public bool Send_fovorite_update { get; set; }
        [Display(Name = "Siųsti pranešimus apie istorijo esančių skelbimus ")]
        public bool Send_history_update { get; set; }
        [Display(Name = "Siųsti pranešimus apie jūsų skelbimo komentavimą")]
        public bool Send_comments_update { get; set; }
        [Display(Name = "Siųsti pranešimus apie surišinėjimus naujus")]
        public bool Send_conversation_update { get; set; }
        [Display(Name = "Siųsti pranešimus apie skelbimus kuriuos stebite")]
        public bool Send_intrest_new_items_updates { get; set; }
        [Display(Name = "Siųsti pranešimus jei pasirodė naujas skelbimas jūsų mieste")]
        public bool Send_notification_about_new_item_in_your_city { get; set; }
        [Display(Name = "Siųsti pranešimus apie susidomėjimą jūsų skelbimu")]
        public bool Send_notification_about_intrest_in_your_item { get; set; }
        [Display(Name = "Siųsti pranešimus apie jūsų skelbimų komentavimą")]
        public bool Send_notification_about_comment_on_your_item { get; set; }

        [Display(Name = "Tikslus adresas")]
        public string Street_adress { get; set; }

        //Page Settings
        [Display(Name = "Skelbimų limitas")]
        public int Adverts_limit { get; set; }
        [Display(Name = "Nuotraukų užimamo dydzio limitas")]
        public int Images_size_limit { get; set; }
        [Display(Name = "Nuotraukų skaičiaus limitas")]
        public int Images_count_limit { get; set; }
        [Display(Name = "Komentarų limitas")]
        public int Use_comment_limit { get; set; }
        [Display(Name = "Žinučių limitas")]
        public int User_message_limit { get; set; }
        [Display(Name = "Skelminų su susidomėjimu limitas")]
        public int User_favorite_items_limit { get; set; }
        [Display(Name = "Užpildyta pilna asmeninė informacija?")]
        public bool HasCompletedPersonalInfo { get; set; }
        [Display(Name = "Ar yra svarbus asmuo?")]
        public bool Is_vip { get; set; }

        //Banned
        [Display(Name = "Ar yra užblokuotas?")]
        public bool Is_banned { get; set; }
        [Display(Name = "Iki kada užblokuotas?")]
        public string Duration_of_ban { get; set; }
        [Display(Name = "Užblokavimo priežastis")]
        public string Reason_of_ban { get; set; }

        //Foreign keys
        [Display(Name = "Išsilavinimas")]
        public int Education { get; set; }
        [Display(Name = "Identifikacinis numeris")]
        public int id { get; set; }
        [Display(Name = "Identifikacinis miesto numeris")]
        public int fk_City_Users { get; set; }
    }
}
