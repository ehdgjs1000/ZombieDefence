using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BackEnd;

public class BackEndGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackEndGameData instance = null;
    public static BackEndGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackEndGameData();
            }
            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private UserQuestData userQuestData = new UserQuestData();
    public UserQuestData UserQuestData => userQuestData; 

    private string gameDataRowInData = string.Empty;
    private string gameQuestDataRowInData = string.Empty;

    /// <summary>
    /// �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        //���� ������ �ʱⰪ���� ����
        userGameData.Reset();
        userQuestData.Reset();

        // ���̺� �߰��� �����ͷ� ����
        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            { "energy", userGameData.energy },
            { "promotion", userGameData.promotionType}
        };

        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", UserQuestData.isRewardReceived[0]},
            {"R2Received", UserQuestData.isRewardReceived[1]},
            {"R3Received", UserQuestData.isRewardReceived[2]},
            {"R4Received", UserQuestData.isRewardReceived[3]},
            {"R5Received", UserQuestData.isRewardReceived[4]},
            {"questClearAmount", UserQuestData.questClearAmount }
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameDataRowInData = callback.GetInDate();
                Debug.Log($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
            else //���н�
            {
                Debug.LogError($"���� ���� ������ ���Կ� �����߽��ϴ� : {callback}");
            }
        });
        Backend.GameData.Insert("USER_QUESTDATA", questParam, callback =>
        {
            if (callback.IsSuccess())
            {
                gameQuestDataRowInData = callback.GetInDate();
                Debug.Log($"����Ʈ ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
            else
            {
                Debug.LogError($"����Ʈ ���� ������ ���Կ� �����߽��ϴ� : {callback}");
            }
        });

    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺��� ���� ������ �ҷ��� �� ȣ��
    /// </summary>
    public void GameDataLoad()
    {
        Debug.Log("Load Server");

        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameDataRowInData = gameDataJson[0]["inDate"].ToString();
                        //�ҷ��� ���� ������ userGameData ������ ����
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.exp = int.Parse(gameDataJson[0]["exp"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.crystal = int.Parse(gameDataJson[0]["crystal"].ToString());
                        userGameData.energy = int.Parse(gameDataJson[0]["energy"].ToString());
                        userGameData.promotionType = int.Parse(gameDataJson[0]["promotion"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    userQuestData.Reset();

                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }

        });
        Backend.GameData.GetMyData("USER_QUESTDATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                try
                {
                    LitJson.JsonData gameQuestDataJson = callback.FlattenRows();

                    if (gameQuestDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        //�ҷ��� ���� ������ ���� ��
                        gameQuestDataRowInData = gameQuestDataJson[0]["inDate"].ToString();
                        //�ҷ��� ���� ������ userGameData ������ ����
                        userQuestData.questCount[0] = int.Parse(gameQuestDataJson[0]["Q1Amount"].ToString());
                        userQuestData.questCount[1] = int.Parse(gameQuestDataJson[0]["Q2Amount"].ToString());
                        userQuestData.questCount[2] = int.Parse(gameQuestDataJson[0]["Q3Amount"].ToString());
                        userQuestData.questCount[3] = int.Parse(gameQuestDataJson[0]["Q4Amount"].ToString());
                        userQuestData.questCount[4] = int.Parse(gameQuestDataJson[0]["Q5Amount"].ToString());
                        userQuestData.questCount[5] = int.Parse(gameQuestDataJson[0]["Q6Amount"].ToString());
                        userQuestData.isReceived[0] = bool.Parse(gameQuestDataJson[0]["Q1Received"].ToString());
                        userQuestData.isReceived[1] = bool.Parse(gameQuestDataJson[0]["Q2Received"].ToString());
                        userQuestData.isReceived[2] = bool.Parse(gameQuestDataJson[0]["Q3Received"].ToString());
                        userQuestData.isReceived[3] = bool.Parse(gameQuestDataJson[0]["Q4Received"].ToString());
                        userQuestData.isReceived[4] = bool.Parse(gameQuestDataJson[0]["Q5Received"].ToString());
                        userQuestData.isReceived[5] = bool.Parse(gameQuestDataJson[0]["Q6Received"].ToString());
                        userQuestData.isReceived[6] = bool.Parse(gameQuestDataJson[0]["Q7Received"].ToString());
                        userQuestData.isRewardReceived[0] = bool.Parse(gameQuestDataJson[0]["R1Received"].ToString());
                        userQuestData.isRewardReceived[1] = bool.Parse(gameQuestDataJson[0]["R2Received"].ToString());
                        userQuestData.isRewardReceived[2] = bool.Parse(gameQuestDataJson[0]["R3Received"].ToString());
                        userQuestData.isRewardReceived[3] = bool.Parse(gameQuestDataJson[0]["R4Received"].ToString());
                        userQuestData.isRewardReceived[4] = bool.Parse(gameQuestDataJson[0]["R5Received"].ToString());
                        userQuestData.questClearAmount = float.Parse(gameQuestDataJson[0]["questClearAmount"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    userQuestData.Reset();

                    Debug.LogError(e);
                }
            }
            else // �������� ��
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }

        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺� �ִ� ���� ������ ����
    /// </summary>
    public void GameDataUpdate(UnityAction action)
    {
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. + " +
                "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        } else if (userQuestData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. + " +
                "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy},
            {"promotion", userGameData.promotionType }
        };
        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", userQuestData.isRewardReceived[0] },
            {"R2Received", userQuestData.isRewardReceived[1] },
            {"R3Received", userQuestData.isRewardReceived[2] },
            {"R4Received", userQuestData.isRewardReceived[3] },
            {"R5Received", userQuestData.isRewardReceived[4] },
            {"questClearAmount", userQuestData.questClearAmount }
        };

        //���� ������ ������(gameDataRowInDate)�� ������ ���� �޼��� ���
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInData, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });

        }
        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameQuestDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, questParam, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });

        }
    }
    public void GameDataUpdate()
    {
        Debug.Log("Update Server");
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. + " +
                "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }else if (userQuestData == null)
        {
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy},
            {"promotion", userGameData.promotionType }
        };
        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", userQuestData.isRewardReceived[0] },
            {"R2Received", userQuestData.isRewardReceived[1] },
            {"R3Received", userQuestData.isRewardReceived[2] },
            {"R4Received", userQuestData.isRewardReceived[3] },
            {"R5Received", userQuestData.isRewardReceived[4] },
            {"questClearAmount", userQuestData.questClearAmount}
        };

        //���� ������ ������(gameDataRowInDate)�� ������ ���� �޼��� ���
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            //Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInData, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });
        }

        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.Log(gameQuestDataRowInData);
            Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameQuestDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, questParam, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });

        }
    }
    public void GameDataReset()
    {
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. + " +
                "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        } else if (userQuestData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. + " +
                "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }

        //���� ������ ������(gameDataRowInDate)�� ������ ���� �޼��� ���
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.DeleteV2("USER_DATA", gameDataRowInData, Backend.UserInDate, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });
        }

        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.LogError("������ inDate������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        //���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate �÷��� ����
        //�����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2()�� ȣ��
        else
        {
            Debug.Log($"{gameQuestDataRowInData}�� �������� ������ ������ ��û�մϴ�.");

            Backend.GameData.DeleteV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
                else
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }

            });
        }
    }
}
