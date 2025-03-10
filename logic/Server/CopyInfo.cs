using GameClass.GameObj;
using GameClass.GameObj.Areas;
using Preparation.Utility;
using Protobuf;
using Utility = Preparation.Utility;

namespace Server
{
    public static class CopyInfo
    {
        public static MessageOfObj? Auto(GameObj gameObj, long time)
        {
            if (gameObj.IsRemoved == true)
                return null;
            switch (gameObj.Type)
            {
                case GameObjType.Character:
                    return Character((Character)gameObj, time);
                case GameObjType.E_Resource:
                    return E_Resource((E_Resource)gameObj);
                case GameObjType.A_Resource:
                    return A_Resource((A_Resource)gameObj);
                case GameObjType.Construction:
                    Construction construction = (Construction)gameObj;
                    if (construction.ConstructionType == Utility.ConstructionType.BARRACKS)
                        return Barracks(construction);
                    else if (construction.ConstructionType == Utility.ConstructionType.SPRING)
                        return Spring(construction);
                    else if (construction.ConstructionType == Utility.ConstructionType.FARM)
                        return Farm(construction);
                    return null;
                case GameObjType.Trap:
                    return Trap((Trap)gameObj);
                default: return null;
            }
        }

        public static MessageOfObj? Auto(MessageOfNews news)
        {
            MessageOfObj objMsg = new()
            {
                NewsMessage = news
            };
            return objMsg;
        }

        private static MessageOfObj? Character(Character player, long time)
        {
            MessageOfObj msg = new()
            {
                CharacterMessage = new()
                {
                    Guid = player.ID,

                    TeamId = player.TeamID,
                    PlayerId = player.PlayerID,


                    CharacterType = player.CharacterType,
                    CharacterState1 = player.CharacterState1,
                    CharacterState2 = player.CharacterState2,

                    X = player.Position.x,
                    Y = player.Position.y,

                    FacingDirection = player.FacingDirection.Angle(),
                    Speed = player.MoveSpeed,
                    ViewRange = player.ViewRange,

                    Atk = player.ATK,
                    AttackSize = player.AttackSize,

                    SkillCD = player.SkillCD,

                    EconomyDepletion = player.EconomyDepletion,
                    KillSvore = player.KillScore,

                    Hp = (int)player.HP,


                    EquipmentType = player.EquipmentType,
                }
            };
            return msg;
        }

        private static MessageOfObj EconomyResource(EconomyResource economyresource)
        {
            MessageOfObj msg = new()
            {
                EconomyResourceMessage = new()
                {
                    State = Transformation.EconomyResourceStateToProto(economyresource.EconomyResourceState),

                    X = economyresource.Position.x,
                    Y = economyresource.Position.y,

                    Progress = (int)economyresource.Progress,
                }
            };
            return msg;
        }

        private static MessageOfObj AdditionResource(AdditionResource additionResource)
        {
            MessageOfObj msg = new()
            {
                AdditionResourceMessage = new()
                {
                    Type = additionResource.A_ResourceType,
                    State = additionResource.AdditionResourceState,

                    X = additionResource.Position.x,
                    Y = additionResource.Position.y,

                    Hp = (int)additionResource.HP,
                }
            };
            //   Debugger.Output(additionResource, additionResource.Place.ToString()+" "+additionResource.Position.ToString());
            return msg;
        }

        private static MessageOfObj Barracks(Construction construction)
        {
            MessageOfObj msg = new()
            {
                BarracksMessage = new()
                {
                    X = construction.Position.x,
                    Y = construction.Position.y,

                    Hp = (int)construction.HP,

                    TeamId = construction.TeamID,
                }
            };
            return msg;
        }

        private static MessageOfObj Spring(Construction construction)
        {
            MessageOfObj msg = new()
            {
                SpringMessage = new()
                {
                    X = construction.Position.x,
                    Y = construction.Position.y,

                    Hp = (int)construction.HP,

                    TeamId = construction.TeamID,
                }
            };
            return msg;
        }

        private static MessageOfObj Farm(Construction construction)
        {
            MessageOfObj msg = new()
            {
                FarmMessage = new()
                {
                    X = construction.Position.x,
                    Y = construction.Position.y,

                    Hp = (int)construction.HP,

                    TeamId = construction.TeamID,
                }
            };
            return msg;
        }

        private static MessageOfObj Trap(Trap trap)
        {
            MessageOfObj msg = new()
            {
                TrapMessage = new()
                {
                    X = trap.Position.x,
                    Y = trap.Position.y,

                    Hp = (int)trap.HP,

                    TeamId = trap.TeamID,
                }
            };
            return msg;
        }
    }
}
