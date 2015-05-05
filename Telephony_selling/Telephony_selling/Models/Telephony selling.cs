using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telephony_selling.Models
{
    public class Items
    {
        public int ItemsID { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Стоимость")]
        public double Price { get; set; }
        [Display(Name = "Осталось")]
        public int count { get; set; }
        public int RateTID { get; set; }
        [Display(Name = "Категория")]
        public RateT RateT { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public List<LoadList> LoadList { get; set; }
        public List<Parameters> Parameters { get; set; }
    }
    public class GroupOfParameter
    {
        public int GroupOfParameterID { get; set; }
        public string NameParameter { get; set; }
        public List<ParamClass> ParamClass { get; set; }
    }
    public class RateT
    {
        public int RateTID { get; set; }
        [Display(Name = "Категория")]
        public string Name { get; set; }
        public List<Items> Items { get; set; }
        public List<ParamList> ParamList { get; set; }
    }
    public class Parameters
    {
        public int ParametersID { get; set; }
        public string ParamValue { get; set; }
        public int ItemsID { get; set; }
        public Items Items { get; set; }
        public int ParamClassID { get; set; }
        public ParamClass ParamClass { get; set; }
    }
    public class ParamClass
    {
        public int ParamClassID { get; set; }
        [Display(Name = "Характеристика")]
        public string ParamClassName { get; set; }
        public List<Parameters> Parameters { get; set; }
        public int ParamRateID { get; set; }
        public ParamRate ParamRate { get; set; }
        public List<ParamList> ParamList { get; set; }
        public int GroupOfParameterID { get; set; }
        [Display(Name = "Группа параметров")]
        public GroupOfParameter GroupOfParameter { get; set; }
    }
    public class ParamList
    {
        public int ParamListID { get; set; }
        public int RateTID { get; set; }
        public RateT RateT { get; set; }
        public int ParamClassID { get; set; }
        public ParamClass ParamClass { get; set; }
    }
    public class ParamRate
    {
        public int ParamRateID { get; set; }
        [Display(Name = "Размерность")]
        public string ParamRateName { get; set; }
        public List<ParamClass> ParamClass { get; set; }
    }
    public class LoadList
    {
        public int LoadListID { get; set; }
        [Display(Name = "Количество")]
        public int count { get; set; }
        [Display(Name = "Товар")]
        public Items Items { get; set; }
        public int ItemsID { get; set; }
        public Load Load { get; set; }
        public int LoadID { get; set; }
    }
    public class Load
    {
        public int LoadID { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Доставка")]
        public bool Crew { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime Date { get; set; }
        public virtual List<LoadList> LoadList { get; set; }
        [Display(Name = "Покупатель")]
        public Client Client { get; set; }
        public int ClientID { get; set; }
        [Display(Name = "Скидка")]
        public Discount Discount { get; set; }
        public int DiscountID { get; set; }
    }
    public class Discount
    {
        public int DiscountID { get; set; }
        [Display(Name = "Размер скидки")]
        public double Value { get; set; }
        [Display(Name = "Начальное значение")]
        public double Mincost { get; set; }
        [Display(Name = "Конечное значение")]
        public double Maxcost { get; set; }
        public virtual List<Load> Load { get; set; }
    }
    public class Client
    {
        public int ClientID { get; set; }
        [Display(Name = "Логин")]
        [Remote("CheckLogin", "Account")]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Display(Name = "Подсказка")]
        public string PasswordHelp { get; set; }
        [Display(Name = "Фамилия")]
        public string Family { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        public string Second { get; set; }
        public Role Role { get; set; }
        public int RoleID { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "E-Mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Mail { get; set; }
        public virtual List<Load> Load { get; set; }
        [NotMapped]
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string Repeat { get; set; }
    }
    public class Role
    {
        public int RoleID { get; set; }
        [Display(Name = "Роль")]
        public string RoleName { get; set; }
        [Display(Name = "Совершение покупок")]
        public bool Buy { get; set; }
        [Display(Name = "Редактирование товаров")]
        public bool EditItems { get; set; }
        [Display(Name = "Редактирование справочной информации")]
        public bool Library { get; set; }
        [Display(Name = "Назначение ролей")]
        public bool Autentification { get; set; }
        [Display(Name = "Редактирование общей информации")]
        public bool Content { get; set; }
        public virtual List<Client> Client { get; set; }
    }
    public class StatusList
    {
        public int StatusListID { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        public Load Load { get; set; }
        public int LoadID { get; set; }
        [Display(Name = "Статус")]
        public StatusType StatusType { get; set; }
        public int StatusTypeID { get; set; }
    }
    public class StatusType
    {
        public int StatusTypeID { get; set; }
        [Display(Name = "Тип статуса")]
        public string StatusName { get; set; }
        public virtual List<StatusList> StatusList { get; set; }
    }
    public class TelephonyContext : DbContext
    {
        public DbSet<Items> Items { get; set; }
        public DbSet<RateT> RateT { get; set; }
        public DbSet<Parameters> Parameters { get; set; }
        public DbSet<ParamClass> ParamClass { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<LoadList> LoadList { get; set; }
        public DbSet<Load> Load { get; set; }
        public DbSet<StatusList> StatusList { get; set; }
        public DbSet<StatusType> StatusType { get; set; }
        public DbSet<GroupOfParameter> GroupOfParameter { get; set; }
        public DbSet<ParamList> ParamList { get; set; }

        public System.Data.Entity.DbSet<Telephony_selling.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<Telephony_selling.Models.ParamRate> ParamRates { get; set; }
    }
}