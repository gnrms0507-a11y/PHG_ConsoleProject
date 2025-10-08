using EnumManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlayerOfSword
{

    public partial class Player        //스킬세팅
    {
       public enum PlayerSkill
        {
            PowerStrike, CrossEdge, BladeTempest ,DragonStrike,SwordJudgement , Back
        }

        
        public Dictionary<string, PlayerSkill> playerskills { get; set; } = new Dictionary<string, PlayerSkill>();//입력받은 key와 Dictionary의 Key일치 여부판단을 위해 string으로 지정
        public Dictionary<PlayerSkill, int> skillDemage { get; set; } = new Dictionary<PlayerSkill, int>(); //스킬의 데미지 입력
        public Dictionary<PlayerSkill, int> skillMp { get; set; } = new Dictionary<PlayerSkill, int>(); //스킬의 소모 mp입력

        public void PlayerBasicSkill()  //플레이어 기본스킬 추가
        { 

            playerskills.Add("1", PlayerSkill.PowerStrike);
            playerskills.Add("2", PlayerSkill.CrossEdge);
            playerskills.Add("3", PlayerSkill.BladeTempest);
            

            skillDemage.Add(PlayerSkill.PowerStrike, power + 15);
            skillDemage.Add(PlayerSkill.CrossEdge, power + 20);
            skillDemage.Add(PlayerSkill.BladeTempest, power + 35);
   

            skillMp.Add(PlayerSkill.PowerStrike, 10);
            skillMp.Add(PlayerSkill.CrossEdge, 15);
            skillMp.Add(PlayerSkill.BladeTempest, 30);

            playerskills.Add((skillDemage.Count+1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
        }
        public void PlayerBasicSkill(Player _player)  //플레이어 기본스킬 추가 - 장비 업데이트에 따른 스킬데미지 업데이트
        {
            playerskills.Clear();
            skillDemage.Clear();
            skillMp.Clear();

            playerskills.Add("1", PlayerSkill.PowerStrike);
            playerskills.Add("2", PlayerSkill.CrossEdge);
            playerskills.Add("3", PlayerSkill.BladeTempest);

            skillDemage.Add(PlayerSkill.PowerStrike, power + 15);
            skillDemage.Add(PlayerSkill.CrossEdge, power + 20);
            skillDemage.Add(PlayerSkill.BladeTempest, power + 35);

            skillMp.Add(PlayerSkill.PowerStrike, 10);
            skillMp.Add(PlayerSkill.CrossEdge, 15);
            skillMp.Add(PlayerSkill.BladeTempest, 30);

            playerskills.Add((skillDemage.Count + 1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
        }

        public int useSkill(string skillKey, out bool? isUseSkill)      //플레이어의 스킬사용
        {
            int playerskillDemage = 0;
            isUseSkill = null;

            foreach(var skill in playerskills)
            {
                if(skill.Key == skillKey)
                {
                    if (skillMp.TryGetValue(skill.Value, out int mp))
                    {
                        isUseSkill = (currentMp - mp >= 0) ? true : false;      //스킬을 사용할수 있으면 true반환.

                        if (isUseSkill==true)       //스킬사용 가능시 사용
                        {
                            for(int i =0; i< ($"{playerName} 의 {skill.Value} !! ").Length; i++)
                            {
                                Console.Write($"{playerName} 의 {skill.Value} !! "[i]);
                                Thread.Sleep(50);
                            }
                            currentMp -= mp;    //플레이어의 현재 mp 차감

                            Thread.Sleep(300);
                        }
                        else
                        {
                            Console.WriteLine("Mp가 부족합니다 사용불가.");
                            Thread.Sleep(700);
                            isUseSkill = false;
                            break;
                        }
                      
                    }

                    if (isUseSkill == true && skillDemage.TryGetValue(skill.Value, out int Demage)==true)  //스킬 데미지 할당
                    {
                        playerskillDemage = Demage;
                        break;
                    }
                 
                    
                }
            }

            return playerskillDemage;
        }


    }
   

    
}
