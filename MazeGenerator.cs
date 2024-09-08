using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // This script was made for a game that I was working on over the summer (Summer 2024), and have since abandoned.
    // It's a slight rework of another maze generator script I was working on weeks earlier, which was standalone and ran in the terminal.

    // roomPrefab is a prefab of a room made entirely of multiple cuboid meshes with textures
    // rooms is the list of rooms generated in the GenerateMaze function below
    // The DisableUnusedRooms function was created to stop the rendering of rooms the player was not currently in, preventing an unnecessary performance drop

    // NOTE: This script won't work without the rest of the game's files, this is simply a display of my work.

    private GameObject player;
    private GameObject roomPrefab;

    public List<GameObject> rooms;

    public int mazeSize;
    private void Awake()
    {
        player = GameObject.Find("Player");
        roomPrefab = Resources.Load("Prefabs/Room") as GameObject;
    }
    public void GenerateMaze(int size, int complexity)
    {
        mazeSize = size;

        int roomNumber = 1;

        // Generate rooms with respective number
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3(-j * 32, 0, i * 32), Quaternion.identity, gameObject.transform);
                rooms.Add(room);

                room.GetComponent<RoomBehaviors>().roomNumber = roomNumber;
                roomNumber++;
            }
        }

        // Randomize room connections based on maze complexity
        for (int i = 0; i < Mathf.Pow(size, 2); i++)
        {
            int rndDoors;

            bool isNorthEdge = false;
            bool isSouthEdge = false;
            bool isEastEdge = false;
            bool isWestEdge = false;

            if (complexity == 1)
            {
                rndDoors = Random.Range(3, 5);
            }
            else if (complexity == 2)
            {
                rndDoors = Random.Range(2, 4);
            }
            else
            {
                rndDoors = Random.Range(1, 3);
            }

            if (i < size)
            {
                isNorthEdge = true;
            }
            if (i >= (size * (size - 1)))
            {
                isSouthEdge = true;
            }
            if (i % size == size - 1)
            {
                isEastEdge = true;
            }
            if (i % size == 0)
            {
                isWestEdge = true;
            }

            if (isNorthEdge && isWestEdge)
            {
                int where = Random.Range(1, 4);

                if (where == 1)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                    rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
                else if (where == 2)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                    rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
                else if (where == 3)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                    rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
            }
            else if (isSouthEdge && isEastEdge)
            {
                int where = Random.Range(1, 4);

                if (where == 1)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                    rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                }
                else if (where == 2)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                    rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
                else if (where == 3)
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                    rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                }
            }

            if (rndDoors == 4)
            {
                if (isNorthEdge)
                {
                    if (isEastEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (!isWestEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    }
                }
                else if (isSouthEdge)
                {
                    if (isWestEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (!isEastEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    }
                }
                else if (isEastEdge && (!isNorthEdge && !isSouthEdge))
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                }
                else if (isWestEdge && (!isNorthEdge && !isSouthEdge))
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
                else
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);
                }
            }
            else if (rndDoors == 3)
            {
                if (isNorthEdge)
                {
                    if (isEastEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (!isWestEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    }
                }
                else if (isSouthEdge)
                {
                    if (isWestEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (!isEastEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    }
                }
                else if (isEastEdge && (!isNorthEdge && !isSouthEdge))
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                    rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                }
                else if (isWestEdge && (!isNorthEdge && !isSouthEdge))
                {
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                    rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                }
                else
                {
                    int where = Random.Range(1, 5);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                    }
                    else if (where == 4)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                }
            }
            else if (rndDoors == 2)
            {
                if (isNorthEdge)
                {
                    if (isEastEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (!isWestEdge)
                    {
                        int where = Random.Range(1, 4);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        }
                        else if (where == 3)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        }
                    }
                }
                else if (isSouthEdge)
                {
                    if (isWestEdge)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (!isEastEdge)
                    {
                        int where = Random.Range(1, 4);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        }
                        else if (where == 3)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        }
                    }
                }
                else if (isEastEdge && (!isNorthEdge && !isSouthEdge))
                {
                    int where = Random.Range(1, 4);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                }
                else if (isWestEdge && (!isNorthEdge && !isSouthEdge))
                {
                    int where = Random.Range(1, 4);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                }
                else
                {
                    int where = Random.Range(1, 7);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (where == 4)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 5)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                    else if (where == 6)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                    }
                }
            }
            else if (rndDoors == 1)
            {
                if (isNorthEdge)
                {
                    if (isEastEdge)
                    {
                        int where = Random.Range(1, 3);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        }
                    }
                    else if (!isWestEdge)
                    {
                        int where = Random.Range(1, 4);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 3)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        }
                    }
                }
                else if (isSouthEdge)
                {
                    if (isWestEdge)
                    {
                        int where = Random.Range(1, 3);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                    }
                    else if (!isEastEdge)
                    {
                        int where = Random.Range(1, 4);

                        if (where == 1)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 2)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                        }
                        else if (where == 3)
                        {
                            rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                            rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                            rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        }
                    }
                }
                else if (isEastEdge && (!isNorthEdge && !isSouthEdge))
                {
                    int where = Random.Range(1, 4);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                }
                else if (isWestEdge && (!isNorthEdge && !isSouthEdge))
                {
                    int where = Random.Range(1, 4);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                }
                else
                {
                    int where = Random.Range(1, 5);

                    if (where == 1)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);

                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 2)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 3)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(false);
                    }
                    else if (where == 4)
                    {
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);

                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(false);
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(false);
                    }
                }
            }
        }

        // Connect rooms based on location in grid
        for (int i = 0; i < Mathf.Pow(size, 2); i++)
        {
            if (i >= size)
            {
                if (rooms[i - size].GetComponent<RoomBehaviors>().doors.Contains(rooms[i - size].transform.Find("DoorwaySouth").gameObject))
                {
                    rooms[i].GetComponent<RoomBehaviors>().isNorthConnected = true;
                    rooms[i].GetComponent<RoomBehaviors>().northConnectedRoom = rooms[i - size];

                    if (rooms[i].transform.Find("DoorwayNorth").gameObject.activeSelf == false)
                    {
                        rooms[i].transform.Find("DoorwayNorth").gameObject.SetActive(true);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayNorth").gameObject);
                    }
                }
            }

            if (i < (size * (size - 1)))
            {
                if (rooms[i + size].GetComponent<RoomBehaviors>().doors.Contains(rooms[i + size].transform.Find("DoorwayNorth").gameObject))
                {
                    rooms[i].GetComponent<RoomBehaviors>().isSouthConnected = true;
                    rooms[i].GetComponent<RoomBehaviors>().southConnectedRoom = rooms[i + size];

                    if (rooms[i].transform.Find("DoorwaySouth").gameObject.activeSelf == false)
                    {
                        rooms[i].transform.Find("DoorwaySouth").gameObject.SetActive(true);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwaySouth").gameObject);
                    }
                }
            }

            if (i % size != size - 1)
            {
                if (rooms[i + 1].GetComponent<RoomBehaviors>().doors.Contains(rooms[i + 1].transform.Find("DoorwayWest").gameObject))
                {
                    rooms[i].GetComponent<RoomBehaviors>().isEastConnected = true;
                    rooms[i].GetComponent<RoomBehaviors>().eastConnectedRoom = rooms[i + 1];

                    if (rooms[i].transform.Find("DoorwayEast").gameObject.activeSelf == false)
                    {
                        rooms[i].transform.Find("DoorwayEast").gameObject.SetActive(true);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayEast").gameObject);
                    }
                }
            }

            if (i % size != 0)
            {
                if (rooms[i - 1].GetComponent<RoomBehaviors>().doors.Contains(rooms[i - 1].transform.Find("DoorwayEast").gameObject))
                {
                    rooms[i].GetComponent<RoomBehaviors>().isWestConnected = true;
                    rooms[i].GetComponent<RoomBehaviors>().westConnectedRoom = rooms[i - 1];

                    if (rooms[i].transform.Find("DoorwayWest").gameObject.activeSelf == false)
                    {
                        rooms[i].transform.Find("DoorwayWest").gameObject.SetActive(true);
                        rooms[i].GetComponent<RoomBehaviors>().doors.Add(rooms[i].transform.Find("DoorwayWest").gameObject);
                    }
                }
            }
        }

        DisableUnusedRooms();
    }
    public void DisableUnusedRooms()
    {
        foreach (GameObject room in rooms)
        {
            if (player.GetComponent<PlayerStats>().roomNumCurrent == room.GetComponent<RoomBehaviors>().roomNumber)
            {
                if (room.activeSelf == false)
                {
                    room.SetActive(true);
                }
            }
            else
            {
                if (room.activeSelf == true)
                {
                    room.SetActive(false);
                }
            }
        }
    }
}