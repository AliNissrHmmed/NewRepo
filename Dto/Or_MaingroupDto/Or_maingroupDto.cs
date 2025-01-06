namespace ERP 
{
    public class AddMainGroupDto
    {


        public string MainGroupName { get; set; } = string.Empty;
        public bool State { get; set; }
        public List<SubgroupDto> Subgroups { get; set; } = new List<SubgroupDto>();
    }

    public class SubgroupDto
    {
        public bool SupTreeGroup { get; set; }
        public bool State { get; set; }
        public Guid SectionId { get; set; }
        public string Note { get; set; } = string.Empty;
        public string TypeItem { get; set; } = string.Empty;
    }





    public class MainGroupWithSubGroupsDto
    {
        public string MainGroupName { get; set; } // اسم المجموعة الرئيسية
        public string MainGroupCode { get; set; } // الكود الخاص بالمجموعة الرئيسية
        public Guid OrganizationId { get; set; } // معرف المنظمة المرتبط بالمجموعة
        public DateTime CreatedAt { get; set; } // تاريخ إنشاء المجموعة الرئيسية
        public bool State { get; set; }
        public List<SubGroupDto> SubGroups { get; set; } = new List<SubGroupDto>();// قائمة المجموعات الفرعية المرتبطة
    }
    public class SubGroupDto
    {
        public string Code { get; set; } // كود المجموعة الفرعية
        public string Note { get; set; } // الوصف أو الملاحظات الخاصة بالمجموعة الفرعية
        public string ItemType { get; set; } // نوع العنصر للمجموعة الفرعية
        public bool SupTreeGroup { get; set; } // هل تنتمي إلى مجموعة فرعية رئيسية أخرى؟
        public Guid SectionId { get; set; } // معرف القسم المرتبط بالمجموعة الفرعية
        public bool State { get; set; } // حالة المجموعة (مفعلة أو غير مفعلة)
        public DateTime CreatedAt { get; set; } // تاريخ إنشاء المجموعة الفرعية
    }











    public class Or_maingroupDto
    {


        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public bool State { get; set; }


    }


    public class Update_maingroupDto
    {


        public string Name { get; set; }
        public string Code { get; set; }
        public bool State { get; set; }


    }

}
