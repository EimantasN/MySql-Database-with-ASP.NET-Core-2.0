using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Database
{
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
        public int Id { get; set; }
        public string Name { get; set; }
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
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImageThumbnail { get; set; }

        //Keys
        public int fk_Item_Images { get; set; }
    }

    public class items
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Description { get; set; }
        public bool Report_likes { get; set; }
        public bool ReportAboutComment { get; set; }
        public bool Report_about_offer { get; set; }
        public int Visited_times { get; set; }
        public int Received_offers { get; set; }
        //Foreign Key Status
        public int Status { get; set; }
        public int Id { get; set; }

        //Keys
        public int fk_Category_Items { get; set; }
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
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime Created { get; set; }

        //Keys
        public int fk_Item_Prices { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Users
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public int Income { get; set; }
        public string Phone_nr { get; set; }
        public string Mobile_nr { get; set; }
        public bool IsComany { get; set; }
        public string Company_name { get; set; }
        public int Age { get; set; }
        public bool Show_email { get; set; }
        public bool Show_phone_nr { get; set; }
        public bool Show_mobile_nr { get; set; }
        public bool Show_location { get; set; }
        public bool Show_the_exact_address { get; set; }
        public bool Allow_send_me_messages { get; set; }
        public int How_often_inform { get; set; }
        public int Which_time_inform { get; set; }
        public bool Send_fovorite_update { get; set; }
        public bool Send_history_update { get; set; }
        public bool Send_comments_update { get; set; }
        public bool Send_conversation_update { get; set; }
        public bool Send_intrest_new_items_updates { get; set; }
        public bool Send_notification_about_new_item_in_your_city { get; set; }
        public bool Send_notification_about_intrest_in_your_item { get; set; }
        public bool Send_notification_about_comment_on_your_item { get; set; }

        public string Street_adress { get; set; }

        //Page Settings
        public int Adverts_limit { get; set; }
        public int Images_size_limit { get; set; }
        public int Images_count_limit { get; set; }
        public int Use_comment_limit { get; set; }
        public int User_message_limit { get; set; }
        public int User_favorite_items_limit { get; set; }
        public bool HasCompletedPersonalInfo { get; set; }
        public bool Is_vip { get; set; }

        //Banned
        public bool Is_banned { get; set; }
        public string Duration_of_ban { get; set; }
        public string Reason_of_ban { get; set; }

        //Foreign keys
        public string Education { get; set; }
        public int id { get; set; }
        public int fk_City_Users { get; set; }
    }
}
