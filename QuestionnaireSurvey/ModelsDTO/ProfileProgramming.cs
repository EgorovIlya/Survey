﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.ModelsDTO
{
    public class ProfileProgramming: Profile
    {
        public ProfileProgramming() : base( 
            new List<ProfileItem>
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО", 0, 300)),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                     , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan),DateTime.Now )),

                new ProfileItem( new QuestionValuesList("Любимый язык программирования"
                    , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
                    , new List<string> {"PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" } )),

                new ProfileItem( new QuestionInt("Опыт программирования на указанном языке"
                    , "Введите опыт программирования на указанном языке (Полных лет)",SurveyConst.MaximumLifeSpan,0)),

                new ProfileItem( new QuestionPhoneNumber("Мобильный телефон", "Укажите номер мобильного телефона")),
            }
            , Guid.NewGuid().ToString())
        {
        }
    }
}
