using EnumManager;
using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SlayerOfSword.Player;


namespace SlayerOfSword
{
    public interface IUseSkill
    {
        void NewSkill(ItemGrade _itemGrade, Player _player);
    }
    public interface IGuardBreak
    {
        void guardBreak(Monster _monster);
    }
    public partial class Item 
    {
        public string itemName { get; set; }
       
        public ItemGrade itemGrade { get; set; }

        public string itemCategory { get; set; }

    }

    public class Weapon : Item
    {
        public int plusPower { get; set; }

        //무기 생성 - 공격력
        public Weapon(WeaponList weaponName,int _power,ItemGrade _itemGrade)
        {
            this.itemName = weaponName.ToString();
            this.plusPower = _power;
            this.itemGrade = _itemGrade;
            this.itemCategory = "Weapon";
        }
    
    }

    //유니크 이상의 무기는 스킬사용이 가능하다
    public class UniqueWeapon : Weapon, IUseSkill
    {
        public UniqueWeapon(WeaponList weaponName, int _power, ItemGrade _itemGrade) : base(weaponName, _power, _itemGrade)
        {
            this.itemName = weaponName.ToString();
            this.plusPower = _power;
            this.itemGrade = ItemGrade.Unique;
            this.itemCategory = "Weapon";
        }
        public void NewSkill(ItemGrade _itemGrade, Player _player)
        {
            //아이템등급이 유니크면 스킬드래곤스트라이크추가함
            if ((int)_itemGrade == (int)ItemGrade.Unique)
            {
                //플레이어 스킬에 드래곤스트라이크가 없을경우 추가함
                if (_player.playerskills.ContainsValue(PlayerSkill.DragonStrike) == false)
                {
                    //플레이어스킬의 제일뒷단(Back 제거)
                    _player.playerskills.Remove(_player.playerskills.Count.ToString());

                    _player.playerskills.Add((_player.playerskills.Count + 1).ToString(), PlayerSkill.DragonStrike);

                    _player.skillDemage.Add(PlayerSkill.DragonStrike, _player.power + 55);

                    _player.skillMp.Add(PlayerSkill.DragonStrike, 48);

                    //플레이어스킬의 제일뒷단에 Back다시추가
                    _player.playerskills.Add((_player.skillDemage.Count + 1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
                }
               
            }
        }
      
    }

    public class LegendWeapon : Weapon, IUseSkill, IGuardBreak
    {
        public LegendWeapon(WeaponList weaponName, int _power, ItemGrade _itemGrade) : base(weaponName, _power, _itemGrade)
        {
            this.itemName = weaponName.ToString();
            this.plusPower = _power;
            this.itemGrade = ItemGrade.Legend;
            this.itemCategory = "Weapon";
        }

        public void NewSkill(ItemGrade _itemGrade, Player _player)
        {
            //아이템등급이 유니크면 드래곤스트라이크 추가함
            if ((int)_itemGrade == (int)ItemGrade.Unique)
            {
                //플레이어 스킬에 드래곤스트라이크가 없을경우 추가함
                if (_player.playerskills.ContainsValue(PlayerSkill.DragonStrike) == false)
                {
                    //플레이어스킬의 제일뒷단(Back 제거)
                    _player.playerskills.Remove(_player.playerskills.Count.ToString());

                    _player.playerskills.Add((_player.playerskills.Count + 1).ToString(), PlayerSkill.DragonStrike);

                    _player.skillDemage.Add(PlayerSkill.DragonStrike, _player.power + 55);

                    _player.skillMp.Add(PlayerSkill.DragonStrike, 48);

                    //플레이어스킬의 제일뒷단에 Back다시추가
                    _player.playerskills.Add((_player.skillDemage.Count + 1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
                }
            }
            //유니크보다 위면 드래곤스트라이크 ,소드저지먼트추가
            else if ((int)_itemGrade > (int)ItemGrade.Unique)
            {
                //플레이어 스킬에 드래곤스트라이크가 없을경우 추가함
                if (_player.playerskills.ContainsValue(PlayerSkill.DragonStrike) == false)
                {
                    //플레이어스킬의 제일뒷단(Back 제거)
                    _player.playerskills.Remove(_player.playerskills.Count.ToString());

                    _player.playerskills.Add((_player.playerskills.Count + 1).ToString(), PlayerSkill.DragonStrike);

                    _player.skillDemage.Add(PlayerSkill.DragonStrike, _player.power + 55);

                    _player.skillMp.Add(PlayerSkill.DragonStrike, 48);

                    //플레이어스킬의 제일뒷단에 Back다시추가
                    _player.playerskills.Add((_player.skillDemage.Count + 1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
                }

                //드래곤스트라이크 이미 있으면 소드저지먼트추가
                if(_player.playerskills.ContainsValue(PlayerSkill.DragonStrike) == true)
                {
                    //플레이어스킬의 제일뒷단(Back 제거)
                    _player.playerskills.Remove(_player.playerskills.Count.ToString());

                    _player.playerskills.Add((_player.playerskills.Count + 1).ToString(), PlayerSkill.SwordJudgement);

                    _player.skillDemage.Add(PlayerSkill.SwordJudgement, _player.power + 75);

                    _player.skillMp.Add(PlayerSkill.SwordJudgement, 60);

                    //플레이어스킬의 제일뒷단에 Back다시추가
                    _player.playerskills.Add((_player.skillDemage.Count + 1).ToString(), PlayerSkill.Back);      //뒤로가기 생성 뒤로가기의 key번호는 스킬의 맨마지막번호가 들어감
                }
            }
        }

        //Legend장비는 몬스터방어력무시
        public void guardBreak(Monster _monster)
        {
            _monster.MonsterDefense = 0;
        }

    }


    public class Armor : Item
    {
        public int plusDefense { get; set; }
        //방어구 생성
        public Armor(ArmorList armorName,int _defense ,ItemGrade _itemGrade)
        {
            this.itemName= armorName.ToString();
            this.plusDefense += _defense;
            this.itemGrade = _itemGrade;
            this.itemCategory = "Armor";
        }

    }



}
