using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ooparts.dungen;

namespace ooparts.dungen
{
	public class Room : MonoBehaviour
	{
		public Corridor CorridorPrefab;
		public IntVector2 Size;
		public IntVector2 Coordinates;
		public int Num;

		private GameObject _tilesObject;
		private GameObject _wallsObject;
        [HideInInspector]
		public  GameObject _monstersObject;

        [HideInInspector]
        public GameObject _pickupObject;

		public Tile TilePrefab;
		private Tile[,] _tiles;
		public GameObject WallPrefab;
        public GameObject WallPrefabTorch;
		public RoomSetting Setting;
        public GameObject treasureChestPrefab;

		public Dictionary<Room, Corridor> RoomCorridor = new Dictionary<Room, Corridor>();//damn das smart B)

		private Map _map;

		public GameObject PlayerPrefab;

		public GameObject MonsterPrefab;
		public int MonsterCount;
		private GameObject[] Monsters;

        public GameObject[] pickupPrefabs;
        public int pickupCount;
        private GameObject[] pickups;

        bool monstersSpawned = false;

        public float distance;
        public Transform playerTransform;

        void Update()
        {
            if (playerTransform != null)//if we have a player object
            {
                if (!monstersSpawned)//if this room has not already spawned its monsters
                {
                    CalculateDistance(transform.position, playerTransform.position);//do a distance calculation between the player and the middle of the room
                    if (distance < Size.x/2 && distance < Size.z/2)//if the player is within the bounds of the room
                    {
                        ActivateMonsters();//activate the monsters
                    }
                }

            }
            else
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void CalculateDistance(Vector3 roomCenter, Vector3 playerPos)
        {
            distance = Vector3.Distance(roomCenter, playerPos);
        }

        public void Init(Map map)
		{
			_map = map;
		}

		public IEnumerator Generate()
		{
			// Create parent object
			_tilesObject = new GameObject("Tiles");
			_tilesObject.transform.parent = transform;
			_tilesObject.transform.localPosition = Vector3.zero;

			_tiles = new Tile[Size.x, Size.z];
			for (int x = 0; x < Size.x; x++)
			{
				for (int z = 0; z < Size.z; z++)
				{
					_tiles[x, z] = CreateTile(new IntVector2((Coordinates.x + x), Coordinates.z + z));
				}
			}
			yield return null;
		}

		private Tile CreateTile(IntVector2 coordinates)
		{
			if (_map.GetTileType(coordinates) == TileType.Empty)
			{
				_map.SetTileType(coordinates, TileType.Room);
			}
			else
			{
				Debug.LogError("Tile Conflict!");
			}
			Tile newTile = Instantiate(TilePrefab);
			newTile.Coordinates = coordinates;
			newTile.name = "Tile " + coordinates.x + ", " + coordinates.z;
			newTile.transform.parent = _tilesObject.transform;
			newTile.transform.localPosition = RoomMapManager.TileSize * new Vector3(coordinates.x - Coordinates.x - Size.x * 0.5f + 0.5f, 0f, coordinates.z - Coordinates.z - Size.z * 0.5f + 0.5f);
			newTile.transform.GetChild(0).GetComponent<Renderer>().material = Setting.floor;
			return newTile;
		}

		public Corridor CreateCorridor(Room otherRoom)
		{
			// Don't create if already connected
			if (RoomCorridor.ContainsKey(otherRoom))
			{
				return RoomCorridor[otherRoom];
			}

			Corridor newCorridor = Instantiate(CorridorPrefab);
			newCorridor.name = "Corridor (" + otherRoom.Num + ", " + Num + ")";
			newCorridor.transform.parent = transform.parent;
			newCorridor.Coordinates = new IntVector2(Coordinates.x + Size.x / 2, otherRoom.Coordinates.z + otherRoom.Size.z / 2);
			newCorridor.transform.localPosition = new Vector3(newCorridor.Coordinates.x - _map.MapSize.x / 2, 0, newCorridor.Coordinates.z - _map.MapSize.z / 2);
			newCorridor.Rooms[0] = otherRoom;
			newCorridor.Rooms[1] = this;
			newCorridor.Length = Vector3.Distance(otherRoom.transform.localPosition, transform.localPosition);
			newCorridor.Init(_map);
			otherRoom.RoomCorridor.Add(this, newCorridor);
			RoomCorridor.Add(otherRoom, newCorridor);

			return newCorridor;
		}

		public IEnumerator CreateWalls()
		{
            bool placedTorchWall = false;
            bool placedWalls = false;

			_wallsObject = new GameObject("Walls");
			_wallsObject.transform.parent = transform;
			_wallsObject.transform.localPosition = Vector3.zero;

			IntVector2 leftBottom = new IntVector2(Coordinates.x - 1, Coordinates.z - 1);
			IntVector2 rightTop = new IntVector2(Coordinates.x + Size.x, Coordinates.z + Size.z);
			for (int x = leftBottom.x; x <= rightTop.x; x++)
			{
				for (int z = leftBottom.z; z <= rightTop.z; z++)
				{
					// If it's center or corner or not wall
					if ((x != leftBottom.x && x != rightTop.x && z != leftBottom.z && z != rightTop.z) ||
						((x == leftBottom.x || x == rightTop.x) && (z == leftBottom.z || z == rightTop.z)) ||
						(_map.GetTileType(new IntVector2(x, z)) != TileType.Wall))
					{
						continue;
					}
					Quaternion rotation = Quaternion.identity;
					if (x == leftBottom.x)
					{
						rotation = MapDirection.West.ToRotation();
					}
					else if (x == rightTop.x)
					{
						rotation = MapDirection.East.ToRotation();
					}
					else if (z == leftBottom.z)
					{
						rotation = MapDirection.South.ToRotation();
					}
					else if (z == rightTop.z)
					{
						rotation = MapDirection.North.ToRotation();
					}
					else
					{
						Debug.LogError("Wall is not on appropriate location!!");
					}

                    GameObject previousWall;

                    if (placedTorchWall)
                    {
                        GameObject newWall = Instantiate(WallPrefab);
                        newWall.name = "Wall (" + x + ", " + z + ")";
                        newWall.transform.parent = _wallsObject.transform;
                        newWall.transform.localPosition = RoomMapManager.TileSize * new Vector3(x - Coordinates.x - Size.x * 0.5f + 0.5f, 0f, z - Coordinates.z - Size.z * 0.5f + 0.5f);
                        newWall.transform.localRotation = rotation;
                        newWall.transform.localScale *= RoomMapManager.TileSize;
                        newWall.transform.GetChild(0).GetComponent<Renderer>().material = Setting.wall;

                        previousWall = newWall;

                        placedTorchWall = false;
                    }
                    else if (!placedTorchWall)
                    {
                        GameObject newWall = Instantiate(WallPrefabTorch);
                        newWall.name = "TorchWall (" + x + ", " + z + ")";
                        newWall.transform.parent = _wallsObject.transform;
                        newWall.transform.localPosition = RoomMapManager.TileSize * new Vector3(x - Coordinates.x - Size.x * 0.5f + 0.5f, 0f, z - Coordinates.z - Size.z * 0.5f + 0.5f);
                        newWall.transform.localRotation = rotation;
                        newWall.transform.localScale *= RoomMapManager.TileSize;
                        newWall.transform.GetChild(0).GetComponent<Renderer>().material = Setting.wall;

                        placedTorchWall = true;
                    }
                    
				}
			}
			yield return null;
		}

		public IEnumerator CreateMonsters()
		{
            if (gameObject.tag != "StairsRoom")
            {
                _monstersObject = new GameObject("Monsters");
                _monstersObject.transform.parent = transform;
                _monstersObject.transform.localPosition = Vector3.zero;

                Monsters = new GameObject[MonsterCount];

                for (int i = 0; i < MonsterCount; i++)
                {
                    GameObject newMonster = Instantiate(MonsterPrefab);
                    newMonster.name = "Monster " + (i + 1);
                    newMonster.transform.parent = _monstersObject.transform;
                    newMonster.transform.localPosition = new Vector3(i / 2f, 0.5f, i % 2f);
                    Monsters[i] = newMonster;
                }
                _monstersObject.SetActive(false);
                yield return null;
            }
		}

        public void ActivateMonsters()
        {
            if (_monstersObject != null)
            {
                _monstersObject.SetActive(true);
            }
            monstersSpawned = true;
        }

        public IEnumerator CreatePickups()
        {
            int pickupChance = Random.Range(0, 100);

            if (pickupChance < 30)
            {
                _pickupObject = new GameObject("Pickups");
                _pickupObject.transform.parent = transform;
                _pickupObject.transform.localPosition = new Vector3(0, 0, 1);

                pickups = new GameObject[pickupCount + 1];

                int whichPickup = Random.Range(0, pickups.Length + 1);

                for (int i = 0; i < pickupCount; i++)
                {
                    //instantiate chest object
                    //assign selected pickup prefab to the chest object
                    GameObject chestObj = Instantiate(treasureChestPrefab);
                    TreasureChest _chest = chestObj.GetComponentInChildren<TreasureChest>();
                    _chest.treasureObj = pickupPrefabs[whichPickup];
                    chestObj.name = "Chest " + (i + 1);
                    chestObj.transform.parent = _pickupObject.transform;
                    chestObj.transform.localPosition = new Vector3(i / 2, 0.5f, i % 2f);
                    pickups[i] = chestObj;

                    /*
                    GameObject newPickup = Instantiate(pickupPrefabs[whichPickup]);
                    newPickup.name = "Pickup " + (i + 1);
                    newPickup.transform.parent = _pickupObject.transform;
                    newPickup.transform.localPosition = new Vector3(i / 2f, 0.5f, i % 2f);
                    pickups[i] = newPickup;*/
                }
            }

            yield return null;
        }

		public IEnumerator CreatePlayer()
		{
            /*buggy as fuck. When loading externally (from menu) it loads player data no matter what. 
             * Fix by changing the parameters to _currFloor >1 and _currFloow <1 respectively.
             * However, the player is no longer child object of the room as expected.
             */
			GameObject player = Instantiate((PlayerPrefab));
            if (RoomMapManager._currFloor > 0)
            {
                player.GetComponent<DataManager>().Load();
                player.name = "Player";
                player.transform.parent = transform.parent;
                player.transform.localPosition = transform.localPosition;
                yield return null;
            }
            else 
            {
                player.name = "Player";
                player.transform.parent = transform.parent;
                player.transform.localPosition = transform.localPosition;
                yield return null;
            }            
		}
	}
}