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

    private string gameDataRowInData = string.Empty;

    /// <summary>
    /// �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        //���� ������ �ʱⰪ���� ����
        userGameData.Reset();

        // ���̺� �߰��� �����ͷ� ����
        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            { "energy", userGameData.energy }
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

    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺��� ���� ������ �ҷ��� �� ȣ��
    /// </summary>
    public void GameDataLoad()
    {
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

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();

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
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy}
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
    }
    public void GameDataUpdate()
    {
        if (userGameData == null)
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
            {"energy", userGameData.energy}
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
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy}
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
    }
}
