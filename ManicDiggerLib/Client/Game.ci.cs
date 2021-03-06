﻿public class Game
{
    public Game()
    {
        one = 1;
        performanceinfo = new DictionaryStringString();
        AudioEnabled = true;
        AutoJumpEnabled = false;
        playerPositionSpawnX = 15 + one / 2;
        playerPositionSpawnY = 64;
        playerPositionSpawnZ = 15 + one / 2;

        TextureId = new int[MaxBlockTypes][];
        for (int i = 0; i < MaxBlockTypes; i++)
        {
            TextureId[i] = new int[6];
        }
        TextureIdForInventory = new int[MaxBlockTypes];
        language = new Language();
        lastplacedblockX = -1;
        lastplacedblockY = -1;
        lastplacedblockZ = -1;
        mLightLevels = new float[16];
        sunlight_ = 15;
        mvMatrix = new StackMatrix4();
        pMatrix = new StackMatrix4();
        mvMatrix.Push(Mat4.Create());
        pMatrix.Push(Mat4.Create());
        whitetexture = -1;
        cachedTextTexturesMax = 1024;
        cachedTextTextures = new CachedTextTexture[cachedTextTexturesMax];
        packetLen = new IntRef();
        ENABLE_DRAW2D = true;
        AllowFreemove = true;
        enableCameraControl = true;
        textures = new DictionaryStringInt1024();
        ServerInfo = new ServerInformation();
        menustate = new MenuState();
        mouseleftclick = false;
        mouseleftdeclick = false;
        wasmouseleft = false;
        mouserightclick = false;
        mouserightdeclick = false;
        wasmouseright = false;
        ENABLE_LAG = 0;
        znear = one / 10;
        CameraMatrix = new GetCameraMatrix();
        ENABLE_ZFAR = true;
        TotalAmmo = new int[GlobalVar.MAX_BLOCKTYPES];
        LoadedAmmo = new int[GlobalVar.MAX_BLOCKTYPES];
        AllowedFontsCount = 1;
        AllowedFonts = new string[AllowedFontsCount];
        AllowedFonts[0] = "Verdana";
        fov = Game.GetPi() / 3;
        cameratype = CameraType.Fpp;
        ENABLE_TPP_VIEW = false;
        basemovespeed = 5;
        movespeed = 5;
        RadiusWhenMoving = one * 3 / 10;
        playervelocity = new Vector3Ref();
        LocalPlayerId = -1;
        dialogs = new VisibleDialog[512];
        dialogsCount = 512;
        blockHealth = new DictionaryVector3Float();
        playertexturedefault = -1;
        a = new AnimationState();
        rotation_speed = one * 180 / 20;
        modmanager = new ClientModManager1();
        particleEffectBlockBreak = new ModDrawParticleEffectBlockBreak();
        PICK_DISTANCE = one * 37 / 10;
        selectedmodelid = -1;
        grenadetime = 3;
        rotationspeed = one * 15 / 100;
        entities = new Entity[entitiesMax];
        entitiesCount = 512;
        PlayerPushDistance = 2;
        keyboardState = new bool[256];
        overheadcameradistance = 10;
        tppcameradistance = 3;
        TPP_CAMERA_DISTANCE_MIN = 1;
        TPP_CAMERA_DISTANCE_MAX = 10;
        options = new OptionsCi();
        overheadcameraK = new Kamera();
        fillarea = new DictionaryVector3Float();
        fillAreaLimit = 200;
        speculativeCount = 0;
        speculative = new Speculative[speculativeMax];
        typinglog = new string[1024 * 16];
        typinglogCount = 0;
        NewBlockTypes = new Packet_BlockType[GlobalVar.MAX_BLOCKTYPES];
        localplayeranim = new AnimationState();
        localplayeranimationhint = new AnimationHint();
        enable_move = true;
        handTexture = -1;
        modelViewInverted = new float[16];
        GLScaleTempVec3 = Vec3.Create();
        GLRotateTempVec3 = Vec3.Create();
        GLTranslateTempVec3 = Vec3.Create();
        identityMatrix = Mat4.Identity_(Mat4.Create());
        Set3dProjectionTempMat4 = Mat4.Create();
        getAsset = new string[1024 * 2];
        PlayerStats = new Packet_ServerPlayerStats();
        mLightLevels = new float[16];
        for (int i = 0; i < 16; i++)
        {
            mLightLevels[i] = one * i / 15;
        }
        scheduler = new TaskScheduler_();
        audiosamples = new DictionaryStringAudioSample();
        soundnow = new BoolRef();
        acceleration = new Acceleration();
        camera = Mat4.Create();
        packetHandlers = new ClientPacketHandler[256];
        player = new Entity();
        player.position = new EntityPosition_();
        player.physicsState = new CharacterPhysicsState();
        currentlyAttackedEntity = -1;
        ChatLinesMax = 1;
        ChatLines = new Chatline[ChatLinesMax];
        ChatLineLength = 64;
    }

    internal AssetList assets;
    internal FloatRef assetsLoadProgress;
    internal TextColorRenderer textColorRenderer;

    public void Start()
    {
        textColorRenderer = new TextColorRenderer();
        textColorRenderer.platform = platform;
        language.platform = platform;
        language.LoadTranslations();
        GameData gamedata = new GameData();
        gamedata.Start();
        Config3d config3d = new Config3d();
        if (platform.IsFastSystem())
        {
            config3d.viewdistance = 128;
        }
        else
        {
            config3d.viewdistance = 32;
        }
        CharacterPhysicsCi physics = new CharacterPhysicsCi();
        //network.d_ResetMap = this;
        ITerrainTextures terrainTextures = new ITerrainTextures();
        terrainTextures.game = this;
        d_TextureAtlasConverter = new TextureAtlasConverter();
        d_TerrainTextures = terrainTextures;
        //InfiniteMapChunked map = new InfiniteMapChunked();// { generator = new WorldGeneratorDummy() };

        FrustumCulling frustumculling = new FrustumCulling();
        frustumculling.d_GetCameraMatrix = this.CameraMatrix;
        frustumculling.platform = platform;
        d_FrustumCulling = frustumculling;

        TerrainChunkTesselatorCi terrainchunktesselator = new TerrainChunkTesselatorCi();
        d_TerrainChunkTesselator = terrainchunktesselator;
        d_Batcher = new MeshBatcher();
        d_Batcher.d_FrustumCulling = frustumculling;
        d_Batcher.game = this;
        d_FrustumCulling = frustumculling;
        //w.d_Map = clientgame.mapforphysics;
        d_Physics = physics;
        d_Data = gamedata;
        d_DataMonsters = new GameDataMonsters();
        d_Config3d = config3d;

        ModDrawParticleEffectBlockBreak particle = new ModDrawParticleEffectBlockBreak();
        this.particleEffectBlockBreak = particle;
        this.d_Data = gamedata;
        d_TerrainTextures = terrainTextures;

        this.Reset(10 * 1000, 10 * 1000, 128);

        //w.d_CurrentShadows = this;
        SunMoonRenderer sunmoonrenderer = new SunMoonRenderer();
        d_SunMoonRenderer = sunmoonrenderer;
        d_SunMoonRenderer = sunmoonrenderer;
        d_Heightmap = new InfiniteMapChunked2d();
        d_Heightmap.d_Map = this;
        d_Heightmap.Restart();
        //this.light = new InfiniteMapChunkedSimple() { d_Map = map };
        //light.Restart();
        d_TerrainChunkTesselator = terrainchunktesselator;
        terrainchunktesselator.game = this;

        //if (fullshadows)
        //{
        //    UseShadowsFull();
        //}
        //else
        //{
        //    UseShadowsSimple();
        //}
        Packet_Inventory inventory = new Packet_Inventory();
        inventory.RightHand = new Packet_Item[10];
        terrainRenderer = new TerrainRenderer();
        terrainRenderer.game = this;
        GameDataItemsClient dataItems = new GameDataItemsClient();
        dataItems.game = this;
        InventoryUtilClient inventoryUtil = new InventoryUtilClient();
        d_Inventory = inventory;
        d_InventoryUtil = inventoryUtil;
        inventoryUtil.d_Inventory = inventory;
        inventoryUtil.d_Items = dataItems;
        d_Physics.game = this;
        d_Inventory = inventory;
        platform.AddOnCrash(OnCrashHandlerLeave.Create(this));

        rnd = platform.RandomCreate();

        clientmods = new ClientMod[128];
        clientmodsCount = 0;
        modmanager.game = this;
        AddMod(new ModAutoCamera());
        AddMod(new ModFpsHistoryGraph());
        AddMod(new ModWalkSound());
        AddMod(new ModFallDamageToPlayer());
        AddMod(new ModBlockDamageToPlayer());
        AddMod(new ModLoadPlayerTextures());
        AddMod(new ModSendPosition());
        AddMod(new ModInterpolatePositions());
        AddMod(new ModRail());

        AddMod(new ModCompass());
        AddMod(new ModGrenade());
        AddMod(new ModBullet());
        AddMod(new ModExpire());
        AddMod(new ModReloadAmmo());
        AddMod(new ModPush());
        if (platform.IsFastSystem())
        {
            AddMod(new ModSkySphereAnimated());
        }
        else
        {
            AddMod(new ModSkySphereStatic());
        }
        AddMod(sunmoonrenderer);
        AddMod(new ModDrawTestModel());
        AddMod(new ModDrawPlayers());
        AddMod(new ModDrawLinesAroundSelectedBlock());
        AddMod(new ModDrawArea());
        AddMod(new ModDrawTerrain());
        AddMod(new ModDrawPlayerNames());
        AddMod(new ModDrawText());
        AddMod(new ModDrawParticleEffectBlockBreak());
        AddMod(new ModDrawSprites());
        AddMod(new ModDrawMinecarts());
        AddMod(new ModDrawHand2d());
        AddMod(new ModDrawHand3d());
        AddMod(new ModGuiCrafting());
        AddMod(new ModDialog());
        AddMod(new ModPicking());
        AddMod(new ModClearInactivePlayersDrawInfo());
        AddMod(new ModCameraKeys());
        AddMod(new ModTrampoline());
        AddMod(new ModSlipperyWalk());
        AddMod(new ModNoAirControl());
        AddMod(new ModCharacterPhysics());
        AddMod(new ModSendActiveMaterial());
        AddMod(new ModCamera());
        AddMod(new ModNetworkEntity());
        AddMod(new ModGuiChat());
        AddMod(new ModGuiInventory());
        AddMod(new ModGuiTouchButtons());
        AddMod(new ModGuiEscapeMenu());
        AddMod(new ModGuiMapLoading());
        AddMod(new ModDraw2dMisc());
        AddMod(new ModScreenshot());

        s = new BlockOctreeSearcher();
        s.platform = platform;

        scheduler.Start(platform);
        DrawTask drawTask = new DrawTask();
        drawTask.game = this;
        QueueTaskReadOnlyMainThread(drawTask);

        UnloadRendererChunks unloadRendererChunks = new UnloadRendererChunks();
        unloadRendererChunks.game = this;
        QueueTaskReadOnlyMainThread(unloadRendererChunks);

        QueueTaskReadOnlyMainThread(terrainRenderer);

        UpdateTask update = new UpdateTask();
        update.game = this;
        QueueTaskCommit(update);

        NetworkProcessTask networkProcessTask = new NetworkProcessTask();
        networkProcessTask.game = this;
        QueueTaskReadOnlyBackgroundPerFrame(networkProcessTask);

        //Prevent loding screen from immediately displaying lag symbol
        LastReceivedMilliseconds = platform.TimeMillisecondsFromStart();

        ENABLE_DRAW_TEST_CHARACTER = platform.IsDebuggerAttached();
    }

#if CITO
    macro Index3d(x, y, h, sizex, sizey) ((((((h) * (sizey)) + (y))) * (sizex)) + (x))
#else
    static int Index3d(int x, int y, int h, int sizex, int sizey)
    {
        return (h * sizey + y) * sizex + x;
    }
#endif

    void AddMod(ClientMod mod)
    {
        clientmods[clientmodsCount++] = mod;
        mod.Start(modmanager);
    }

    internal float one;

    const int MaxBlockTypes = 1024;

    internal GamePlatform platform;
    internal Packet_BlockType[] blocktypes;
    internal Language language;
    internal TerrainChunkTesselatorCi d_TerrainChunkTesselator;

    internal Chunk[] chunks;
    internal int MapSizeX;
    internal int MapSizeY;
    internal int MapSizeZ;
    internal const int chunksize = 16;
    internal const int chunksizebits = 4;

    internal Entity player;

    public bool IsFluid(Packet_BlockType block)
    {
        return block.DrawType == Packet_DrawTypeEnum.Fluid;
    }

    public bool IsRail(Packet_BlockType block)
    {
        return block.Rail > 0;	//Does not include Rail0, but this can't be placed.
    }

    public bool IsEmptyForPhysics(Packet_BlockType block)
    {
        return (block.DrawType == Packet_DrawTypeEnum.Ladder)
            || (block.WalkableType != Packet_WalkableTypeEnum.Solid && block.WalkableType != Packet_WalkableTypeEnum.Fluid);
    }

    public int GetBlock(int x, int y, int z)
    {
        if (!IsValidPos(x, y, z))
        {
            return 0;
        }
        return GetBlockValid(x, y, z);
    }

    public int GetBlockValid(int x, int y, int z)
    {
        int cx = x >> chunksizebits;
        int cy = y >> chunksizebits;
        int cz = z >> chunksizebits;
        int chunkpos = Index3d(cx, cy, cz, MapSizeX >> chunksizebits, MapSizeY >> chunksizebits);
        if (chunks[chunkpos] == null)
        {
            return 0;
        }
        else
        {
            int pos = Index3d(x & (chunksize - 1), y & (chunksize - 1), z & (chunksize - 1), chunksize, chunksize);
            return GetBlockInChunk(chunks[chunkpos], pos);
        }
    }

    public int GetBlockInChunk(Chunk chunk, int pos)
    {
        if (chunk.dataInt != null)
        {
            return chunk.dataInt[pos];
        }
        else
        {
            return chunk.data[pos];
        }
    }

    public void SetBlockRaw(int x, int y, int z, int tileType)
    {
        Chunk chunk = GetChunk(x, y, z);
        int pos = Index3d(x % chunksize, y % chunksize, z % chunksize, chunksize, chunksize);
        SetBlockInChunk(chunk, pos, tileType);
    }

    public void SetBlockInChunk(Chunk chunk, int pos, int block)
    {
        if (chunk.dataInt == null)
        {
            if (block < 255)
            {
                chunk.data[pos] = IntToByte(block);
            }
            else
            {
                int n = chunksize * chunksize * chunksize;
                chunk.dataInt = new int[n];
                for (int i = 0; i < n; i++)
                {
                    chunk.dataInt[i] = chunk.data[i];
                }
                chunk.data = null;

                chunk.dataInt[pos] = block;
            }
        }
        else
        {
            chunk.dataInt[pos] = block;
        }
    }

    internal bool ChunkHasData(Chunk chunk)
    {
        return chunk.data != null || chunk.dataInt != null;
    }

    public Chunk GetChunk(int x, int y, int z)
    {
        x = x / chunksize;
        y = y / chunksize;
        z = z / chunksize;
        int mapsizexchunks = MapSizeX / chunksize;
        int mapsizeychunks = MapSizeY / chunksize;
        Chunk chunk = chunks[Index3d(x, y, z, mapsizexchunks, mapsizeychunks)];
        if (chunk == null)
        {
            Chunk c = new Chunk();
            c.data = new byte[chunksize * chunksize * chunksize];
            chunks[Index3d(x, y, z, mapsizexchunks, mapsizeychunks)] = c;
            return chunks[Index3d(x, y, z, mapsizexchunks, mapsizeychunks)];
        }
        return chunk;
    }

    public bool IsValidPos(int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0)
        {
            return false;
        }
        if (x >= MapSizeX || y >= MapSizeY || z >= MapSizeZ)
        {
            return false;
        }
        return true;
    }

    public int blockheight(int x, int y, int z_)
    {
        for (int z = z_; z >= 0; z--)
        {
            if (GetBlock(x, y, z) != 0)
            {
                return z + 1;
            }
        }
        return 0;
    }

    public bool IsValidChunkPos(int cx, int cy, int cz, int chunksize_)
    {
        return cx >= 0 && cy >= 0 && cz >= 0
            && cx < MapSizeX / chunksize_
            && cy < MapSizeY / chunksize_
            && cz < MapSizeZ / chunksize_;
    }

    public void CopyChunk(Chunk chunk, int[] output)
    {
        int n = chunksize * chunksize * chunksize;
        if (chunk.dataInt != null)
        {
            for (int i = 0; i < n; i++)
            {
                output[i] = chunk.dataInt[i];
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                output[i] = chunk.data[i];
            }
        }
    }

    public static byte IntToByte(int a)
    {
#if CITO
        return a.LowByte;
#else
        return (byte)a;
#endif
    }

    public static int ColorFromArgb(int a, int r, int g, int b)
    {
        int iCol = (a << 24) | (r << 16) | (g << 8) | b;
        return iCol;
    }

    public static int ColorA(int color)
    {
        byte a = IntToByte(color >> 24);
        return a;
    }

    public static int ColorR(int color)
    {
        byte r = IntToByte(color >> 16);
        return r;
    }

    public static int ColorG(int color)
    {
        byte g = IntToByte(color >> 8);
        return g;
    }

    public static int ColorB(int color)
    {
        byte b = IntToByte(color);
        return b;
    }

    public static float GetPi()
    {
        float a = 3141592;
        return a / 1000000;
    }

    //Indexed by block id and TileSide.
    internal int[][] TextureId;
    internal int[] TextureIdForInventory;

    internal int terrainTexturesPerAtlas;

    internal static int MaxInt(int a, int b)
    {
        if (a >= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    internal static int MinInt(int a, int b)
    {
        if (a <= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public void GetMapPortion(int[] outPortion, int x, int y, int z, int portionsizex, int portionsizey, int portionsizez)
    {
        int outPortionCount = portionsizex * portionsizey * portionsizez;
        for (int i = 0; i < outPortionCount; i++)
        {
            outPortion[i] = 0;
        }

        //int chunksizebits = p.FloatToInt(p.MathLog(chunksize, 2));
        if (chunksize != 16)
        {
            platform.ThrowException("GetMapPortion");
        }

        int mapchunksx = MapSizeX / chunksize;
        int mapchunksy = MapSizeY / chunksize;
        int mapchunksz = MapSizeZ / chunksize;
        int mapsizechunks = mapchunksx * mapchunksy * mapchunksz;

        for (int xx = 0; xx < portionsizex; xx++)
        {
            for (int yy = 0; yy < portionsizey; yy++)
            {
                for (int zz = 0; zz < portionsizez; zz++)
                {
                    //Find chunk.
                    int cx = (x + xx) >> chunksizebits;
                    int cy = (y + yy) >> chunksizebits;
                    int cz = (z + zz) >> chunksizebits;
                    //int cpos = MapUtil.Index3d(cx, cy, cz, MapSizeX / chunksize, MapSizeY / chunksize);
                    int cpos = (cz * mapchunksy + cy) * mapchunksx + cx;
                    //if (cpos < 0 || cpos >= ((MapSizeX / chunksize) * (MapSizeY / chunksize) * (MapSizeZ / chunksize)))
                    if (cpos < 0 || cpos >= mapsizechunks)
                    {
                        continue;
                    }
                    Chunk chunk = chunks[cpos];
                    if (chunk == null || !ChunkHasData(chunk))
                    {
                        continue;
                    }
                    //int pos = MapUtil.Index3d((x + xx) % chunksize, (y + yy) % chunksize, (z + zz) % chunksize, chunksize, chunksize);
                    int chunkGlobalX = cx << chunksizebits;
                    int chunkGlobalY = cy << chunksizebits;
                    int chunkGlobalZ = cz << chunksizebits;

                    int inChunkX = (x + xx) - chunkGlobalX;
                    int inChunkY = (y + yy) - chunkGlobalY;
                    int inChunkZ = (z + zz) - chunkGlobalZ;

                    //int pos = MapUtil.Index3d(inChunkX, inChunkY, inChunkZ, chunksize, chunksize);
                    int pos = (((inChunkZ << chunksizebits) + inChunkY) << chunksizebits) + inChunkX;

                    int block = GetBlockInChunk(chunk, pos);
                    //outPortion[MapUtil.Index3d(xx, yy, zz, portionsizex, portionsizey)] = (byte)block;
                    outPortion[(zz * portionsizey + yy) * portionsizex + xx] = block;
                }
            }
        }
    }
    internal int texturesPacked() { return GlobalVar.MAX_BLOCKTYPES_SQRT; } //16x16
    internal int terrainTexture;
    internal int[] terrainTextures1d;
    internal ITerrainTextures d_TerrainTextures;

    internal int lastplacedblockX;
    internal int lastplacedblockY;
    internal int lastplacedblockZ;

    internal InfiniteMapChunked2d d_Heightmap;
    internal Config3d d_Config3d;

    //maps light level (0-15) to GL.Color value.
    internal float[] mLightLevels;
    internal MeshBatcher d_Batcher;
    internal int sunlight_;

    public void Draw2dTexture(int textureid, float x1, float y1, float width, float height, IntRef inAtlasId, int atlastextures, int color, bool enabledepthtest)
    {
        if (color == ColorFromArgb(255, 255, 255, 255) && inAtlasId == null)
        {
            Draw2dTextureSimple(textureid, x1, y1, width, height, enabledepthtest);
        }
        else
        {
            Draw2dTextureInAtlas(textureid, x1, y1, width, height, inAtlasId, atlastextures, color, enabledepthtest);
        }
    }

    Model quadModel;
    void Draw2dTextureSimple(int textureid, float x1, float y1, float width, float height, bool enabledepthtest)
    {
        RectFRef rect = RectFRef.Create(0, 0, 1, 1);
        platform.GlDisableCullFace();
        platform.GlEnableTexture2d();
        platform.BindTexture2d(textureid);

        if (!enabledepthtest)
        {
            platform.GlDisableDepthTest();
        }
        if (quadModel == null)
        {
            quadModel = platform.CreateModel(QuadModelData.GetQuadModelData());
        }
        GLPushMatrix();
        GLTranslate(x1, y1, 0);
        GLScale(width, height, 0);
        GLScale(one / 2, one / 2, 0);
        GLTranslate(one, one, 0);

        DrawModel(quadModel);
        GLPopMatrix();

        if (!enabledepthtest)
        {
            platform.GlEnableDepthTest();
        }
        platform.GlEnableCullFace();
        platform.GlEnableTexture2d();
    }

    void Draw2dTextureInAtlas(int textureid, float x1, float y1, float width, float height, IntRef inAtlasId, int atlastextures, int color, bool enabledepthtest)
    {
        RectFRef rect = RectFRef.Create(0, 0, 1, 1);
        if (inAtlasId != null)
        {
            TextureAtlasCi.TextureCoords2d(inAtlasId.value, atlastextures, rect);
        }
        platform.GlDisableCullFace();
        platform.GlEnableTexture2d();
        platform.BindTexture2d(textureid);

        if (!enabledepthtest)
        {
            platform.GlDisableDepthTest();
        }
        ModelData data = QuadModelData.GetQuadModelData2(rect.x, rect.y, rect.w, rect.h,
            x1, y1, width, height, Game.IntToByte(Game.ColorR(color)), Game.IntToByte(Game.ColorG(color)), Game.IntToByte(Game.ColorB(color)), Game.IntToByte(Game.ColorA(color)));
        DrawModelData(data);
        if (!enabledepthtest)
        {
            platform.GlEnableDepthTest();
        }
        platform.GlEnableCullFace();
        platform.GlEnableTexture2d();
    }

    public ModelData CombineModelData(ModelData[] modelDatas, int count)
    {
        ModelData ret = new ModelData();
        int totalIndices = 0;
        int totalVertices = 0;
        for (int i = 0; i < count; i++)
        {
            ModelData m = modelDatas[i];
            totalIndices += m.indicesCount;
            totalVertices += m.verticesCount;
        }
        ret.indices = new int[totalIndices];
        ret.xyz = new float[totalVertices * 3];
        ret.uv = new float[totalVertices * 2];
        ret.rgba = new byte[totalVertices * 4];

        for (int i = 0; i < count; i++)
        {
            ModelData m = modelDatas[i];
            int retVerticesCount = ret.verticesCount;
            int retIndicesCount = ret.indicesCount;
            for (int k = 0; k < m.indicesCount; k++)
            {
                ret.indices[ret.indicesCount++] = m.indices[k] + retVerticesCount;
            }
            for (int k = 0; k < m.verticesCount * 3; k++)
            {
                ret.xyz[retVerticesCount * 3 + k] = m.xyz[k];
            }
            for (int k = 0; k < m.verticesCount * 2; k++)
            {
                ret.uv[retVerticesCount * 2 + k] = m.uv[k];
            }
            for (int k = 0; k < m.verticesCount * 4; k++)
            {
                ret.rgba[retVerticesCount * 4 + k] = m.rgba[k];
            }
            ret.verticesCount += m.verticesCount;
        }
        return ret;
    }

    public void Draw2dTextures(Draw2dData[] todraw, int todrawLength, int textureid)
    {
        ModelData[] modelDatas = new ModelData[512];
        int modelDatasCount = 0;
        for (int i = 0; i < todrawLength; i++)
        {
            Draw2dData d = todraw[i];
            float x1 = d.x1;
            float y1 = d.y1;
            float width = d.width;
            float height = d.height;
            IntRef inAtlasId = d.inAtlasId;
            int textureId = textureid;
            int color = d.color;

            RectFRef rect = RectFRef.Create(0, 0, 1, 1);
            if (inAtlasId != null)
            {
                TextureAtlasCi.TextureCoords2d(inAtlasId.value, texturesPacked(), rect);
            }

            ModelData modelData =
                QuadModelData.GetQuadModelData2(rect.x, rect.y, rect.w, rect.h,
                x1, y1, width, height, Game.IntToByte(ColorR(color)), Game.IntToByte(ColorG(color)), Game.IntToByte(ColorB(color)), Game.IntToByte(ColorA(color)));
            modelDatas[modelDatasCount++] = modelData;
        }

        ModelData combined = CombineModelData(modelDatas, modelDatasCount);

        platform.GlDisableCullFace();
        platform.GlEnableTexture2d();
        platform.BindTexture2d(textureid);

        platform.GlDisableDepthTest();

        DrawModelData(combined);

        platform.GlEnableDepthTest();

        platform.GlDisableCullFace();
        platform.GlEnableTexture2d();
    }

    internal bool currentMatrixModeProjection;
    internal StackMatrix4 mvMatrix;
    internal StackMatrix4 pMatrix;

    public void GLMatrixModeModelView()
    {
        currentMatrixModeProjection = false;
    }

    public void GLMatrixModeProjection()
    {
        currentMatrixModeProjection = true;
    }

    public void SetMatrixUniforms()
    {
        platform.SetMatrixUniformProjection(pMatrix.Peek());
        platform.SetMatrixUniformModelView(mvMatrix.Peek());
    }

    public void SetMatrixUniformProjection()
    {
        platform.SetMatrixUniformProjection(pMatrix.Peek());
    }

    public void SetMatrixUniformModelView()
    {
        platform.SetMatrixUniformModelView(mvMatrix.Peek());
    }

    public void GLLoadMatrix(float[] m)
    {
        if (currentMatrixModeProjection)
        {
            if (pMatrix.Count() > 0)
            {
                pMatrix.Pop();
            }
            pMatrix.Push(m);
        }
        else
        {
            if (mvMatrix.Count() > 0)
            {
                mvMatrix.Pop();
            }
            mvMatrix.Push(m);
        }
    }

    public void GLPopMatrix()
    {
        if (currentMatrixModeProjection)
        {
            if (pMatrix.Count() > 1)
            {
                pMatrix.Pop();
            }
        }
        else
        {
            if (mvMatrix.Count() > 1)
            {
                mvMatrix.Pop();
            }
        }
    }

    float[] GLScaleTempVec3;
    public void GLScale(float x, float y, float z)
    {
        float[] m;
        if (currentMatrixModeProjection)
        {
            m = pMatrix.Peek();
        }
        else
        {
            m = mvMatrix.Peek();
        }
        Vec3.Set(GLScaleTempVec3, x, y, z);
        Mat4.Scale(m, m, GLScaleTempVec3);
    }

    float[] GLRotateTempVec3;
    public void GLRotate(float angle, float x, float y, float z)
    {
        angle /= 360;
        angle *= 2 * Game.GetPi();
        float[] m;
        if (currentMatrixModeProjection)
        {
            m = pMatrix.Peek();
        }
        else
        {
            m = mvMatrix.Peek();
        }
        Vec3.Set(GLRotateTempVec3, x, y, z);
        Mat4.Rotate(m, m, angle, GLRotateTempVec3);
    }

    float[] GLTranslateTempVec3;
    public void GLTranslate(float x, float y, float z)
    {
        float[] m;
        if (currentMatrixModeProjection)
        {
            m = pMatrix.Peek();
        }
        else
        {
            m = mvMatrix.Peek();
        }
        Vec3.Set(GLTranslateTempVec3, x, y, z);
        Mat4.Translate(m, m, GLTranslateTempVec3);
    }

    public void GLPushMatrix()
    {
        if (currentMatrixModeProjection)
        {
            pMatrix.Push(pMatrix.Peek());
        }
        else
        {
            mvMatrix.Push(mvMatrix.Peek());
        }
    }

    float[] identityMatrix;
    public void GLLoadIdentity()
    {
        if (currentMatrixModeProjection)
        {
            if (pMatrix.Count() > 0)
            {
                pMatrix.Pop();
            }
            pMatrix.Push(identityMatrix);
        }
        else
        {
            if (mvMatrix.Count() > 0)
            {
                mvMatrix.Pop();
            }
            mvMatrix.Push(identityMatrix);
        }
    }

    public void GLOrtho(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        if (currentMatrixModeProjection)
        {
            float[] m = pMatrix.Peek();
            Mat4.Ortho(m, left, right, bottom, top, zNear, zFar);
        }
        else
        {
            platform.ThrowException("GLOrtho");
        }
    }

    public void OrthoMode(int width, int height)
    {
        //GL.Disable(EnableCap.DepthTest);
        GLMatrixModeProjection();
        GLPushMatrix();
        GLLoadIdentity();
        GLOrtho(0, width, height, 0, 0, 1);
        SetMatrixUniformProjection();

        GLMatrixModeModelView();
        GLPushMatrix();
        GLLoadIdentity();
        SetMatrixUniformModelView();
    }

    public void PerspectiveMode()
    {
        // Enter into our projection matrix mode
        GLMatrixModeProjection();
        // Pop off the last matrix pushed on when in projection mode (Get rid of ortho mode)
        GLPopMatrix();
        SetMatrixUniformProjection();

        // Go back to our model view matrix like normal
        GLMatrixModeModelView();
        GLPopMatrix();
        SetMatrixUniformModelView();
        //GL.LoadIdentity();
        //GL.Enable(EnableCap.DepthTest);
    }

    public int WhiteTexture()
    {
        if (this.whitetexture == -1)
        {
            BitmapCi bmp = platform.BitmapCreate(1, 1);
            int[] pixels = new int[1];
            pixels[0] = ColorFromArgb(255, 255, 255, 255);
            platform.BitmapSetPixelsArgb(bmp, pixels);
            this.whitetexture = platform.LoadTextureFromBitmap(bmp);
        }
        return this.whitetexture;
    }
    int whitetexture;

    public static float MinFloat(float a, float b)
    {
        if (a <= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public static float AbsFloat(float b)
    {
        if (b >= 0)
        {
            return b;
        }
        else
        {
            return 0 - b;
        }
    }

    public static float MaxFloat(float a, float b)
    {
        if (a >= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public float getblockheight(int x, int y, int z)
    {
        float RailHeight = one * 3 / 10;
        if (!IsValidPos(x, y, z))
        {
            return 1;
        }
        if (blocktypes[GetBlock(x, y, z)].Rail != 0)
        {
            return RailHeight;
        }
        if (blocktypes[GetBlock(x, y, z)].DrawType == Packet_DrawTypeEnum.HalfHeight)
        {
            return one / 2;
        }
        if (blocktypes[GetBlock(x, y, z)].DrawType == Packet_DrawTypeEnum.Flat)
        {
            return one / 20;
        }
        return 1;
    }

    internal CachedTextTexture[] cachedTextTextures;
    internal int cachedTextTexturesMax;

    public void DeleteUnusedCachedTextTextures()
    {
        int now = platform.TimeMillisecondsFromStart();
        for (int i = 0; i < cachedTextTexturesMax; i++)
        {
            CachedTextTexture t = cachedTextTextures[i];
            if (t == null)
            {
                continue;
            }
            if ((one * (now - t.texture.lastuseMilliseconds) / 1000) > 1)
            {
                platform.GLDeleteTexture(t.texture.textureId);
                cachedTextTextures[i] = null;
            }
        }
    }

    CachedTexture GetCachedTextTexture(Text_ t)
    {
        for (int i = 0; i < cachedTextTexturesMax; i++)
        {
            CachedTextTexture ct = cachedTextTextures[i];
            if (ct == null)
            {
                continue;
            }
            if (ct.text.Equals_(t))
            {
                return ct.texture;
            }
        }
        return null;
    }
    
    public void UpdateTextRendererFont()
    {
        platform.SetTextRendererFont(Font);
    }

    public void Draw2dText(string text, FontCi font, float x, float y, IntRef color, bool enabledepthtest)
    {
        if (text == null || platform.StringTrim(text) == "")
        {
            return;
        }
        if (color == null) { color = IntRef.Create(Game.ColorFromArgb(255, 255, 255, 255)); }
        Text_ t = new Text_();
        t.text = text;
        t.color = color.value;
        t.fontsize = font.size;
        t.fontfamily = font.family;
        t.fontstyle = font.style;
        CachedTexture ct;

        if (GetCachedTextTexture(t) == null)
        {
            ct = MakeTextTexture(t);
            if (ct == null)
            {
                return;
            }
            for (int i = 0; i < cachedTextTexturesMax; i++)
            {
                if (cachedTextTextures[i] == null)
                {
                    CachedTextTexture ct1 = new CachedTextTexture();
                    ct1.text = t;
                    ct1.texture = ct;
                    cachedTextTextures[i] = ct1;
                    break;
                }
            }
        }

        ct = GetCachedTextTexture(t);
        ct.lastuseMilliseconds = platform.TimeMillisecondsFromStart();
        platform.GLDisableAlphaTest();
        Draw2dTexture(ct.textureId, x, y, ct.sizeX, ct.sizeY, null, 0, Game.ColorFromArgb(255, 255, 255, 255), enabledepthtest);
        platform.GLEnableAlphaTest();
        DeleteUnusedCachedTextTextures();
    }

    CachedTexture MakeTextTexture(Text_ t)
    {
        CachedTexture ct = new CachedTexture();
        BitmapCi bmp = textColorRenderer.CreateTextTexture(t);
        ct.sizeX = platform.BitmapGetWidth(bmp);
        ct.sizeY = platform.BitmapGetHeight(bmp);
        ct.textureId = platform.LoadTextureFromBitmap(bmp);
        platform.BitmapDelete(bmp);
        return ct;
    }

    internal float FloorFloat(float a)
    {
        if (a >= 0)
        {
            return platform.FloatToInt(a);
        }
        else
        {
            return platform.FloatToInt(a) - 1;
        }
    }

    public byte[] Serialize(Packet_Client packet, IntRef retLength)
    {
        CitoMemoryStream ms = new CitoMemoryStream();
        Packet_ClientSerializer.Serialize(ms, packet);
        byte[] data = ms.ToArray();
        retLength.value = ms.Length();
        return data;
    }

    public void SendPacket(byte[] packet, int packetLength)
    {
        //try
        //{
        INetOutgoingMessage msg = new INetOutgoingMessage();
        msg.Write(packet, packetLength);
        main.SendMessage(msg, MyNetDeliveryMethod.ReliableOrdered);
        //}
        //catch
        //{
        //    game.p.ConsoleWriteLine("SendPacket error");
        //}
    }

    internal NetClient main;

    IntRef packetLen;
    public void SendPacketClient(Packet_Client packetClient)
    {
        byte[] packet = Serialize(packetClient, packetLen);
        SendPacket(packet, packetLen.value);
    }

    internal bool IsTeamchat;
    internal void SendChat(string s)
    {
        SendPacketClient(ClientPackets.Chat(s, IsTeamchat ? 1 : 0));
    }

    internal void SendPingReply()
    {
        SendPacketClient(ClientPackets.PingReply());
    }

    internal void SendSetBlock(int x, int y, int z, int mode, int type, int materialslot)
    {
        SendPacketClient(ClientPackets.SetBlock(x,y,z,mode,type, materialslot));
    }
    internal int ActiveMaterial;

    internal void SendFillArea(int startx, int starty, int startz, int endx, int endy, int endz, int blockType)
    {
        SendPacketClient(ClientPackets.FillArea(startx, starty, startz, endx, endy, endz, blockType, ActiveMaterial));
    }

    internal void InventoryClick(Packet_InventoryPosition pos)
    {
        SendPacketClient(ClientPackets.InventoryClick(pos));
    }

    internal void WearItem(Packet_InventoryPosition from, Packet_InventoryPosition to)
    {
        SendPacketClient(ClientPackets.WearItem(from, to));
    }

    internal void MoveToInventory(Packet_InventoryPosition from)
    {
        SendPacketClient(ClientPackets.MoveToInventory(from));
    }

    internal DictionaryStringString performanceinfo;


    internal Chatline[] ChatLines;
    internal int ChatLinesMax;
    internal int ChatLinesCount;
    internal int ChatLineLength;
    internal string GuiTypingBuffer;
    internal bool IsTyping;

    public void AddChatline(string s)
    {
        Game game = this;
        if (game.platform.StringEmpty(s))
        {
            return;
        }
        //Check for links in chatline
        bool containsLink = false;
        string linkTarget = "";
        //Normal HTTP links
        if (game.platform.StringContains(s, "http://"))
        {
            containsLink = true;
            IntRef r = new IntRef();
            string[] temp = game.platform.StringSplit(s, " ", r);
            for (int i = 0; i < r.value; i++)
            {
                if (game.platform.StringIndexOf(temp[i], "http://") != -1)
                {
                    linkTarget = temp[i];
                    break;
                }
            }
        }
        //Secure HTTPS links
        if (game.platform.StringContains(s, "https://"))
        {
            containsLink = true;
            IntRef r = new IntRef();
            string[] temp = game.platform.StringSplit(s, " ", r);
            for (int i = 0; i < r.value; i++)
            {
                if (game.platform.StringIndexOf(temp[i], "https://") != -1)
                {
                    linkTarget = temp[i];
                    break;
                }
            }
        }
        int now = game.platform.TimeMillisecondsFromStart();
        //Display message in multiple lines if it's longer than one line
        if (s.Length > ChatLineLength)
        {
            for (int i = 0; i <= s.Length / ChatLineLength; i++)
            {
                int displayLength = ChatLineLength;
                if (s.Length - (i * ChatLineLength) < ChatLineLength)
                {
                    displayLength = s.Length - (i * ChatLineLength);
                }
                if (containsLink)
                    ChatLinesAdd(Chatline.CreateClickable(StringTools.StringSubstring(game.platform, s, i * ChatLineLength, displayLength), now, linkTarget));
                else
                    ChatLinesAdd(Chatline.Create(StringTools.StringSubstring(game.platform, s, i * ChatLineLength, displayLength), now));
            }
        }
        else
        {
            if (containsLink)
                ChatLinesAdd(Chatline.CreateClickable(s, now, linkTarget));
            else
                ChatLinesAdd(Chatline.Create(s, now));
        }
    }

    void ChatLinesAdd(Chatline chatline)
    {
        if (ChatLinesCount >= ChatLinesMax)
        {
            Chatline[] lines2 = new Chatline[ChatLinesMax * 2];
            for (int i = 0; i < ChatLinesMax; i++)
            {
                lines2[i] = ChatLines[i];
            }
            ChatLines = lines2;
            ChatLinesMax *= 2;
        }
        ChatLines[ChatLinesCount++] = chatline;
    }

    internal bool ENABLE_DRAW2D;
    internal bool ENABLE_FREEMOVE;
    internal bool ENABLE_NOCLIP;
    internal bool AllowFreemove;
    internal bool enableCameraControl;

    internal void Respawn()
    {
        SendPacketClient(ClientPackets.SpecialKeyRespawn());
        player.physicsState.movedz = 0;
    }

    public static bool IsTransparentForLight(Packet_BlockType b)
    {
        return b.DrawType != Packet_DrawTypeEnum.Solid && b.DrawType != Packet_DrawTypeEnum.ClosedDoor;
    }

    internal GuiState guistate;
    internal bool overheadcamera;
    public bool GetFreeMouse()
    {
        if (overheadcamera)
        {
            return true;
        }
        return !platform.IsMousePointerLocked();
    }
    public void SetFreeMouse(bool value)
    {
        if (value)
        {
            platform.ExitMousePointerLock();
        }
        else
        {
            platform.RequestMousePointerLock();
        }
    }
    internal MapLoadingProgressEventArgs maploadingprogress;

    public void MapLoadingStart()
    {
        guistate = GuiState.MapLoading;
        SetFreeMouse(true);
        maploadingprogress = new MapLoadingProgressEventArgs();
        fontMapLoading = FontCi.Create("Arial", 14, 0);
    }

    internal FontCi fontMapLoading;

    internal string invalidVersionDrawMessage;
    internal Packet_Server invalidVersionPacketIdentification;

    DictionaryStringInt1024 textures;
    internal int GetTexture(string p)
    {
        if (!textures.Contains(p))
        {
            BoolRef found = new BoolRef();
            BitmapCi bmp = platform.BitmapCreateFromPng(GetFile(p), GetFileLength(p));
            int texture = platform.LoadTextureFromBitmap(bmp);
            textures.Set(p, texture);
            platform.BitmapDelete(bmp);
        }
        return textures.Get(p);
    }

    internal int GetTextureOrLoad(string name, BitmapCi bmp)
    {
        if (!textures.Contains(name))
        {
            BoolRef found = new BoolRef();
            textures.Set(name, platform.LoadTextureFromBitmap(bmp));
        }
        return textures.Get(name);
    }

    internal int xcenter(float width)
    {
        return platform.FloatToInt((platform.GetCanvasWidth() / 2 - width / 2));
    }

    internal int ycenter(float height)
    {
        return platform.FloatToInt((platform.GetCanvasHeight() / 2 - height / 2));
    }

    internal ServerInformation ServerInfo;
    internal bool AudioEnabled;
    internal bool AutoJumpEnabled;
    internal MenuState menustate;
    internal bool mouseleftclick;
    internal bool mouseleftdeclick;
    internal bool wasmouseleft;
    internal bool mouserightclick;
    internal bool mouserightdeclick;
    internal bool wasmouseright;
    internal int ENABLE_LAG;

    public int Width()
    {
        return platform.GetCanvasWidth();
    }

    public int Height()
    {
        return platform.GetCanvasHeight();
    }

    internal float znear;

    internal GetCameraMatrix CameraMatrix;

    float[] Set3dProjectionTempMat4;
    public void Set3dProjection(float zfar, float fov)
    {
        float aspect_ratio = one * Width() / Height();
        Mat4.Perspective(Set3dProjectionTempMat4, fov, aspect_ratio, znear, zfar);
        CameraMatrix.lastpmatrix = Set3dProjectionTempMat4;
        GLMatrixModeProjection();
        GLLoadMatrix(Set3dProjectionTempMat4);
        SetMatrixUniformProjection();
    }
    internal bool ENABLE_ZFAR;

    internal float zfar()
    {
        if (d_Config3d.viewdistance >= 256)
        {
            return d_Config3d.viewdistance * 2;
        }
        return ENABLE_ZFAR ? d_Config3d.viewdistance : 99999;
    }

    internal Packet_ServerPlayerStats PlayerStats;

    internal int[] TotalAmmo;
    internal int[] LoadedAmmo;

    string[] AllowedFonts;
    int AllowedFontsCount;

    internal string ValidFont(string family)
    {
        for (int i = 0; i < AllowedFontsCount; i++)
        {
            if (AllowedFonts[i] == family)
            {
                return family;
            }
        }
        return AllowedFonts[0];
    }

    internal int SelectedBlockPositionX;
    internal int SelectedBlockPositionY;
    internal int SelectedBlockPositionZ;
    internal int SelectedEntityId;

    internal bool IsWater(int blockType)
    {
        string name = blocktypes[blockType].Name;
        if (name == null)
        {
            return false;
        }
        return platform.StringContains(name, "Water"); // todo
    }

    internal int mouseCurrentX;
    internal int mouseCurrentY;
    internal Packet_Inventory d_Inventory;

    internal float fov;

    internal float currentfov()
    {
        if (IronSights)
        {
            Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
            if (item != null && item.ItemClass == Packet_ItemClassEnum.Block)
            {
                if (DeserializeFloat(blocktypes[item.BlockId].IronSightsFovFloat) != 0)
                {
                    return this.fov * DeserializeFloat(blocktypes[item.BlockId].IronSightsFovFloat);
                }
            }
        }
        return this.fov;
    }

    internal bool IronSights;

    internal float DeserializeFloat(int value)
    {
        return (one * value) / 32;
    }

    internal int BlockUnderPlayer()
    {
        if (!IsValidPos(platform.FloatToInt(player.position.x),
            platform.FloatToInt(player.position.z),
            platform.FloatToInt(player.position.y) - 1))
        {
            return -1;
        }
        int blockunderplayer = GetBlock(platform.FloatToInt(player.position.x),
            platform.FloatToInt(player.position.z),
            platform.FloatToInt(player.position.y) - 1);
        return blockunderplayer;
    }

    internal CameraType cameratype;
    internal bool ENABLE_TPP_VIEW;

    internal Vector3Ref playerdestination;
    internal void SetCamera(CameraType type)
    {
        if (type == CameraType.Fpp)
        {
            cameratype = CameraType.Fpp;
            SetFreeMouse(false);
            ENABLE_TPP_VIEW = false;
            overheadcamera = false;
        }
        else if (type == CameraType.Tpp)
        {
            cameratype = CameraType.Tpp;
            ENABLE_TPP_VIEW = true;
        }
        else
        {
            cameratype = CameraType.Overhead;
            overheadcamera = true;
            SetFreeMouse(true);
            ENABLE_TPP_VIEW = true;
            playerdestination = Vector3Ref.Create(player.position.x, player.position.y, player.position.z);
        }
    }
    internal float basemovespeed;
    internal float movespeed;
    internal float BuildDelay()
    {
        float default_ = (one * 95 / 100) * (1 / basemovespeed);
        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item == null || item.ItemClass != Packet_ItemClassEnum.Block)
        {
            return default_;
        }
        float delay = DeserializeFloat(blocktypes[item.BlockId].DelayFloat);
        if (delay == 0)
        {
            return default_;
        }
        return delay;
    }

    internal Packet_InventoryPosition InventoryPositionMaterialSelector(int materialId)
    {
        Packet_InventoryPosition pos = new Packet_InventoryPosition();
        pos.Type = Packet_InventoryPositionTypeEnum.MaterialSelector;
        pos.MaterialId = materialId;
        return pos;
    }

    internal Packet_InventoryPosition InventoryPositionMainArea(int x, int y)
    {
        Packet_InventoryPosition pos = new Packet_InventoryPosition();
        pos.Type = Packet_InventoryPositionTypeEnum.MainArea;
        pos.AreaX = x;
        pos.AreaY = y;
        return pos;
    }

    internal RailDirection PickHorizontalVertical(float xfract, float yfract)
    {
        float x = xfract;
        float y = yfract;
        if (y >= x && y >= (1 - x))
        {
            return RailDirection.Vertical;
        }
        if (y < x && y < (1 - x))
        {
            return RailDirection.Vertical;
        }
        return RailDirection.Horizontal;
    }

    internal RailDirection PickCorners(float xfract, float zfract)
    {
        float half = one / 2;
        if (xfract < half && zfract < half)
        {
            return RailDirection.UpLeft;
        }
        if (xfract >= half && zfract < half)
        {
            return RailDirection.UpRight;
        }
        if (xfract < half && zfract >= half)
        {
            return RailDirection.DownLeft;
        }
        return RailDirection.DownRight;
    }

    internal IntRef BlockInHand()
    {
        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item != null && item.ItemClass == Packet_ItemClassEnum.Block)
        {
            return IntRef.Create(item.BlockId);
        }
        return null;
    }

    internal float RadiusWhenMoving;

    internal float CurrentRecoil()
    {
        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item == null || item.ItemClass != Packet_ItemClassEnum.Block)
        {
            return 0;
        }
        return DeserializeFloat(blocktypes[item.BlockId].RecoilFloat);
    }
    internal Vector3Ref playervelocity;

    internal float CurrentAimRadius()
    {
        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item == null || item.ItemClass != Packet_ItemClassEnum.Block)
        {
            return 0;
        }
        float radius = (DeserializeFloat(blocktypes[item.BlockId].AimRadiusFloat) / 800) * Width();
        if (IronSights)
        {
            radius = (DeserializeFloat(blocktypes[item.BlockId].IronSightsAimRadiusFloat) / 800) * Width();
        }
        return radius + RadiusWhenMoving * radius * (Game.MinFloat(playervelocity.Length() / movespeed, 1));
    }

    internal RandomCi rnd;

    internal PointFloatRef GetAim()
    {
        if (CurrentAimRadius() <= 1)
        {
            return PointFloatRef.Create(0, 0);
        }
        float half = one / 2;
        float x;
        float y;
        for (; ; )
        {
            x = (rnd.NextFloat() - half) * CurrentAimRadius() * 2;
            y = (rnd.NextFloat() - half) * CurrentAimRadius() * 2;
            float dist1 = platform.MathSqrt(x * x + y * y);
            if (dist1 <= CurrentAimRadius())
            {
                break;
            }
        }
        return PointFloatRef.Create(x, y);
    }

    public static float ClampFloat(float value, float min, float max)
    {
        float result = value;
        if (value > max)
        {
            result = max;
        }
        if (value < min)
        {
            result = min;
        }
        return result;
    }

    public static int ClampInt(int value, int min, int max)
    {
        int result = value;
        if (value > max)
        {
            result = max;
        }
        if (value < min)
        {
            result = min;
        }
        return result;
    }

    internal GameData d_Data;
    internal TerrainRenderer terrainRenderer;

    public const int maxlight = 15;

    public int MaybeGetLight(int x, int y, int z)
    {
        IntRef ret = terrainRenderer.MaybeGetLight(x, y, z);
        if (ret == null)
        {
            return maxlight;
        }
        return ret.value;
    }

    public void Draw2dBitmapFile(string filename, float x, float y, float w, float h)
    {
        Draw2dTexture(GetTexture(filename), x, y, w, h, null, 0, ColorFromArgb(255, 255, 255, 255), false);
    }
    internal int maxdrawdistance;
    public void ToggleFog()
    {
        int[] drawDistances = new int[10];
        int drawDistancesCount = 0;
        drawDistances[drawDistancesCount++] = 32;
        if (maxdrawdistance >= 64) { drawDistances[drawDistancesCount++] = 64; }
        if (maxdrawdistance >= 128) { drawDistances[drawDistancesCount++] = 128; }
        if (maxdrawdistance >= 256) { drawDistances[drawDistancesCount++] = 256; }
        if (maxdrawdistance >= 512) { drawDistances[drawDistancesCount++] = 512; }
        for (int i = 0; i < drawDistancesCount; i++)
        {
            if (d_Config3d.viewdistance == drawDistances[i])
            {
                d_Config3d.viewdistance = drawDistances[(i + 1) % drawDistancesCount];
                terrainRenderer.StartTerrain();
                return;
            }
        }
        d_Config3d.viewdistance = drawDistances[0];
        terrainRenderer.StartTerrain();
    }

    internal int LocalPlayerId;

    internal float GetCharacterEyesHeight()
    {
        return entities[LocalPlayerId].drawModel.eyeHeight;
    }

    internal void SetCharacterEyesHeight(float value)
    {
        entities[LocalPlayerId].drawModel.eyeHeight = value;
    }

    public float EyesPosX() { return player.position.x; }
    public float EyesPosY() { return player.position.y + GetCharacterEyesHeight(); }
    public float EyesPosZ() { return player.position.z; }

    public void AudioPlay(string file)
    {
        if (!AudioEnabled)
        {
            return;
        }
        AudioPlayAt(file, EyesPosX(), EyesPosY(), EyesPosZ());
    }

    DictionaryStringAudioSample audiosamples;
    AudioSampleCi GetAudioSample(string file)
    {
        if (!audiosamples.Contains(file))
        {
            byte[] data = GetFile(file);
            AudioSampleCi sample = platform.AudioLoad(data);
            audiosamples.Set(file, sample);
        }

        AudioSampleCi ret = audiosamples.Get(file);
        return ret;
    }

    public void AudioPlayAt(string file, float x, float y, float z)
    {
        if (file == null)
        {
            return;
        }
        if (!AudioEnabled)
        {
            return;
        }
        string file_ = platform.StringReplace(file, ".wav", ".ogg");

        if (GetFileLength(file_) == 0)
        {
            platform.ConsoleWriteLine(platform.StringFormat("File not found: {0}", file));
            return;
        }

        AudioSampleCi sample = GetAudioSample(file_);
        platform.AudioPlay(sample, EyesPosX(), EyesPosY(), EyesPosZ());
    }

    public void AudioPlayLoop(string file, bool play, bool restart)
    {
        if ((!AudioEnabled) && play)
        {
            return;
        }

        string file_ = platform.StringReplace(file, ".wav", ".ogg");

        if (GetFileLength(file_) == 0)
        {
            platform.ConsoleWriteLine(platform.StringFormat("File not found: {0}", file));
            return;
        }

        AudioSampleCi sample = GetAudioSample(file_);

        platform.AudioPlayLoop(sample, play, restart);
    }

    public int MaterialSlots(int i)
    {
        Packet_Item item = d_Inventory.RightHand[i];
        int m = d_Data.BlockIdDirt();
        if (item != null && item.ItemClass == Packet_ItemClassEnum.Block)
        {
            m = d_Inventory.RightHand[i].BlockId;
        }
        return m;
    }

    internal bool IsTileEmptyForPhysics(int x, int y, int z)
    {
        if (z >= MapSizeZ)
        {
            return true;
        }
        if (x < 0 || y < 0 || z < 0)// || z >= mapsizez)
        {
            return ENABLE_FREEMOVE;
        }
        if (x >= MapSizeX || y >= MapSizeY)// || z >= mapsizez)
        {
            return ENABLE_FREEMOVE;
        }
        int block = GetBlockValid(x, y, z);
        return block == SpecialBlockId.Empty
            || block == d_Data.BlockIdFillArea()
            || IsWater(block);
    }

    internal bool IsTileEmptyForPhysicsClose(int x, int y, int z)
    {
        return IsTileEmptyForPhysics(x, y, z)
            || (IsValidPos(x, y, z) && blocktypes[GetBlock(x, y, z)].DrawType == Packet_DrawTypeEnum.HalfHeight)
            || (IsValidPos(x, y, z) && IsEmptyForPhysics(blocktypes[GetBlock(x, y, z)]));
    }

    internal bool IsUsableBlock(int blocktype)
    {
        return d_Data.IsRailTile(blocktype) || blocktypes[blocktype].IsUsable;
    }

    internal bool IsWearingWeapon()
    {
        return d_Inventory.RightHand[ActiveMaterial] != null;
    }

    internal void ApplyDamageToPlayer(int damage, int damageSource, int sourceId)
    {
        PlayerStats.CurrentHealth -= damage;
        if (PlayerStats.CurrentHealth <= 0)
        {
            AudioPlay("death.wav");
            SendPacketClient(ClientPackets.Death(damageSource, sourceId));

            //Respawn(); //Death is not respawn ;)
        }
        else
        {
            AudioPlay(rnd.Next() % 2 == 0 ? "grunt1.wav" : "grunt2.wav");
        }
        SendPacketClient(ClientPackets.Health(PlayerStats.CurrentHealth));
    }
    
    public int GetPlayerEyesBlockX()
    {
        return platform.FloatToInt(MathFloor(player.position.x));
    }
    public int GetPlayerEyesBlockY()
    {
        return platform.FloatToInt(MathFloor(player.position.z));
    }
    public int GetPlayerEyesBlockZ()
    {
        return platform.FloatToInt(MathFloor(player.position.y + entities[LocalPlayerId].drawModel.eyeHeight));
    }

    public int MathFloor(float a)
    {
        if (a >= 0)
        {
            return platform.FloatToInt(a);
        }
        else
        {
            return platform.FloatToInt(a) - 1;
        }
    }

    internal void SetChunksAroundDirty(int cx, int cy, int cz)
    {
        if (IsValidChunkPos(cx, cy, cz, chunksize)) { terrainRenderer.SetChunkDirty(cx - 1, cy, cz, true, false); }
        if (IsValidChunkPos(cx - 1, cy, cz, chunksize)) { terrainRenderer.SetChunkDirty(cx - 1, cy, cz, true, false); }
        if (IsValidChunkPos(cx + 1, cy, cz, chunksize)) { terrainRenderer.SetChunkDirty(cx + 1, cy, cz, true, false); }
        if (IsValidChunkPos(cx, cy - 1, cz, chunksize)) { terrainRenderer.SetChunkDirty(cx, cy - 1, cz, true, false); }
        if (IsValidChunkPos(cx, cy + 1, cz, chunksize)) { terrainRenderer.SetChunkDirty(cx, cy + 1, cz, true, false); }
        if (IsValidChunkPos(cx, cy, cz - 1, chunksize)) { terrainRenderer.SetChunkDirty(cx, cy, cz - 1, true, false); }
        if (IsValidChunkPos(cx, cy, cz + 1, chunksize)) { terrainRenderer.SetChunkDirty(cx, cy, cz + 1, true, false); }
    }

    internal void Reset(int sizex, int sizey, int sizez)
    {
        MapSizeX = sizex;
        MapSizeY = sizey;
        MapSizeZ = sizez;
        chunks = new Chunk[(sizex / chunksize) * (sizey / chunksize) * (sizez / chunksize)];
        // SetAllChunksNotDirty();
    }

    internal void UpdateColumnHeight(int x, int y)
    {
        //todo faster
        int height = MapSizeZ - 1;
        for (int i = MapSizeZ - 1; i >= 0; i--)
        {
            height = i;
            if (!Game.IsTransparentForLight(blocktypes[GetBlock(x, y, i)]))
            {
                break;
            }
        }
        d_Heightmap.SetBlock(x, y, height);
    }

    internal void ShadowsOnSetBlock(int x, int y, int z)
    {
        int oldheight = d_Heightmap.GetBlock(x, y);
        UpdateColumnHeight(x, y);
        //update shadows in all chunks below
        int newheight = d_Heightmap.GetBlock(x, y);
        int min = Game.MinInt(oldheight, newheight);
        int max = Game.MaxInt(oldheight, newheight);
        for (int i = min; i < max; i++)
        {
            if (i / chunksize != z / chunksize)
            {
                terrainRenderer.SetChunkDirty(x / chunksize, y / chunksize, i / chunksize, true, false);
            }
        }
        //Todo: too many redraws. Optimize.
        //Now placing a single block updates 27 chunks,
        //and each of those chunk updates calculates light from 27 chunks.
        //So placing a block is often 729x slower than it should be.
        for (int xx = 0; xx < 3; xx++)
        {
            for (int yy = 0; yy < 3; yy++)
            {
                for (int zz = 0; zz < 3; zz++)
                {
                    int cx = x / chunksize + xx - 1;
                    int cy = y / chunksize + yy - 1;
                    int cz = z / chunksize + zz - 1;
                    if (IsValidChunkPos(cx, cy, cz, chunksize))
                    {
                        terrainRenderer.SetChunkDirty(cx, cy, cz, true, false);
                    }
                }
            }
        }
    }

    internal void SetBlock(int x, int y, int z, int tileType)
    {
        SetBlockRaw(x, y, z, tileType);
        terrainRenderer.SetChunkDirty(x / chunksize, y / chunksize, z / chunksize, true, true);
        //d_Shadows.OnSetBlock(x, y, z);
        ShadowsOnSetBlock(x, y, z);
        lastplacedblockX = x;
        lastplacedblockY = y;
        lastplacedblockZ = z;
    }

    internal VisibleDialog[] dialogs;
    internal int dialogsCount;

    internal int DialogsCount()
    {
        int count = 0;
        for (int i = 0; i < dialogsCount; i++)
        {
            if (dialogs[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    internal int GetDialogId(string name)
    {
        for (int i = 0; i < dialogsCount; i++)
        {
            if (dialogs[i] == null)
            {
                continue;
            }
            if (dialogs[i].key == name)
            {
                return i;
            }
        }
        return -1;
    }

    internal DictionaryVector3Float blockHealth;

    internal float GetCurrentBlockHealth(int x, int y, int z)
    {
        if (blockHealth.ContainsKey(x, y, z))
        {
            return blockHealth.Get(x, y, z);
        }
        int blocktype = GetBlock(x, y, z);
        return d_Data.Strength()[blocktype];
    }

    internal Vector3IntRef currentAttackedBlock;
    internal int currentlyAttackedEntity;

    internal void SendRequestBlob(string[] required, int requiredCount)
    {
        SendPacketClient(ClientPackets.RequestBlob(this, required, requiredCount));
    }

    internal int currentTimeMilliseconds;
    internal GameDataMonsters d_DataMonsters;
    internal int ReceivedMapLength;

    void InvalidPlayerWarning(int playerid)
    {
        platform.ConsoleWriteLine(platform.StringFormat("Position update of nonexistent player {0}.", platform.IntToString(playerid)));
    }

    internal bool EnablePlayerUpdatePosition(int kKey)
    {
        return true;
    }

    internal bool EnablePlayerUpdatePositionContainsKey(int kKey)
    {
        return false;
    }

    internal byte localstance;
    internal bool spawned;

    internal int MapLoadingPercentComplete;
    internal string MapLoadingStatus;
    internal int LastReceivedMilliseconds;
    internal int playertexturedefault;
    public const string playertexturedefaultfilename = "mineplayer.png";
    internal bool ENABLE_DRAW_TEST_CHARACTER;
    internal AnimationState a;
    internal ModSkySphereStatic skysphere;
    internal int reloadblock;
    internal int reloadstartMilliseconds;
    internal int lastOxygenTickMilliseconds;
    internal int typinglogpos;
    internal TypingState GuiTyping;
    internal ConnectData connectdata;
    internal bool issingleplayer;
    internal bool IsShiftPressed;
    internal bool reconnect;
    internal bool exitToMainMenu;
    internal float rotation_speed;
    internal void SendLeave(int reason)
    {
        SendPacketClient(ClientPackets.Leave(reason));
    }
    internal IFrustumCulling d_FrustumCulling;
    internal CharacterPhysicsCi d_Physics;
    internal ClientModManager1 modmanager;
    internal ClientMod[] clientmods;
    internal int clientmodsCount;
    internal bool SkySphereNight;
    internal ModDrawParticleEffectBlockBreak particleEffectBlockBreak;
    internal int lastchunkupdates;
    internal int lasttitleupdateMilliseconds;
    internal bool ENABLE_DRAWPOSITION;

    public int SerializeFloat(float p)
    {
        return platform.FloatToInt(p * 32);
    }

    public float WeaponAttackStrength()
    {
        return NextFloat(2, 4);
    }

    public float NextFloat(float min, float max)
    {
        return rnd.NextFloat() * (max - min) + min;
    }

    public byte HeadingByte(float orientationX, float orientationY, float orientationZ)
    {
        return Game.IntToByte(platform.FloatToInt((((orientationY) % (2 * Game.GetPi())) / (2 * Game.GetPi())) * 256));
    }

    public byte PitchByte(float orientationX, float orientationY, float orientationZ)
    {
        float xx = (orientationX + Game.GetPi()) % (2 * Game.GetPi());
        xx = xx / (2 * Game.GetPi());
        return Game.IntToByte(platform.FloatToInt(xx * 256));
    }

    public void PlaySoundAt(string name, float x, float y, float z)
    {
        if (x == 0 && y == 0 && z == 0)
        {
            AudioPlay(name);
        }
        else
        {
            AudioPlayAt(name, x, z, y);
        }
    }

    internal void InvokeMapLoadingProgress(int progressPercent, int progressBytes, string status)
    {
        maploadingprogress = new MapLoadingProgressEventArgs();
        maploadingprogress.ProgressPercent = progressPercent;
        maploadingprogress.ProgressBytes = progressBytes;
        maploadingprogress.ProgressStatus = status;
    }

    internal void Log(string p)
    {
        AddChatline(p);
    }

    internal void SetTileAndUpdate(int x, int y, int z, int type)
    {
        SetBlock(x, y, z, type);
        RedrawBlock(x, y, z);
    }

    internal void RedrawBlock(int x, int y, int z)
    {
        terrainRenderer.RedrawBlock(x, y, z);
    }

    internal bool IsFillBlock(int blocktype)
    {
        return blocktype == d_Data.BlockIdFillArea()
            || blocktype == d_Data.BlockIdFillStart()
            || blocktype == d_Data.BlockIdCuboid();
    }

    internal bool IsAnyPlayerInPos(int blockposX, int blockposY, int blockposZ)
    {
        for (int i = 0; i < entitiesCount; i++)
        {
            if (entities[i] == null)
            {
                continue;
            }
            if (entities[i].drawModel == null)
            {
                continue;
            }
            Entity p = entities[i];
            if (p.networkPosition == null || (p.networkPosition != null && p.networkPosition.PositionLoaded))
            {
                if (IsPlayerInPos(p.position.x, p.position.y, p.position.z,
                    blockposX, blockposY, blockposZ))
                {
                    return true;
                }
            }
        }
        return IsPlayerInPos(player.position.x, player.position.y, player.position.z,
            blockposX, blockposY, blockposZ);
    }

    bool IsPlayerInPos(float playerposX, float playerposY, float playerposZ,
                       int blockposX, int blockposY, int blockposZ)
    {
        if (FloorFloat(playerposX) == blockposX
            &&
            (FloorFloat(playerposY + (one / 2)) == blockposZ
             || FloorFloat(playerposY + 1 + (one / 2)) == blockposZ)
            && FloorFloat(playerposZ) == blockposY)
        {
            return true;
        }
        return false;
    }
    internal float PICK_DISTANCE;
    internal bool leftpressedpicking;
    internal int selectedmodelid;
    internal int pistolcycle;
    internal int lastironsightschangeMilliseconds;
    internal int grenadecookingstartMilliseconds;
    internal float grenadetime;
    internal int lastpositionsentMilliseconds;

    internal float mouseDeltaX;
    internal float mouseDeltaY;
    float rotationspeed;
    internal void UpdateMouseViewportControl(float dt)
    {
        if (!overheadcamera)
        {
            player.position.roty += mouseDeltaX * rotationspeed * (one / 75);
            player.position.rotx += mouseDeltaY * rotationspeed * (one / 75);
            player.position.rotx = Game.ClampFloat(player.position.rotx,
                Game.GetPi() / 2 + (one * 15 / 1000),
                (Game.GetPi() / 2 + Game.GetPi() - (one * 15 / 1000)));

            player.position.rotx += touchOrientationDy * rotation_speed * (one / 75);
            player.position.roty += touchOrientationDx * rotation_speed * (one / 75);
            touchOrientationDx = 0;
            touchOrientationDy = 0;
        }
        if (guistate == GuiState.Normal && platform.Focused() && cameratype == CameraType.Overhead)
        {
            if (mouseMiddle || mouseRight)
            {
                overheadcameraK.TurnLeft(mouseDeltaX / 70);
                overheadcameraK.TurnUp(mouseDeltaY / 3);
            }
        }
    }

    internal string Follow;
    internal IntRef FollowId()
    {
        for (int i = 0; i < entitiesCount; i++)
        {
            if (entities[i] == null)
            {
                continue;
            }
            if (entities[i].drawName == null)
            {
                continue;
            }
            DrawName p = entities[i].drawName;
            if (p.Name == Follow)
            {
                return IntRef.Create(i);
            }
        }
        return null;
    }

    public float Dist(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        float dz = z2 - z1;
        return platform.MathSqrt(dx * dx + dy * dy + dz * dz);
    }

    internal bool IsValid(int blocktype)
    {
        return blocktypes[blocktype].Name != null;
    }

    internal int TextSizeWidth(string s, int size)
    {
        IntRef width = new IntRef();
        IntRef height = new IntRef();
        platform.TextSize(s, size, width, height);
        return width.value;
    }

    internal int TextSizeHeight(string s, int size)
    {
        IntRef width = new IntRef();
        IntRef height = new IntRef();
        platform.TextSize(s, size, width, height);
        return height.value;
    }

    ModelData circleModelData;
    public void Circle3i(float x, float y, float radius)
    {
        float angle;
        GLPushMatrix();
        GLLoadIdentity();

        int n = 32;
        if (circleModelData == null)
        {
            circleModelData = new ModelData();
            circleModelData.setMode(DrawModeEnum.Lines);
            circleModelData.indices = new int[n * 2];
            circleModelData.xyz = new float[3 * n];
            circleModelData.rgba = new byte[4 * n];
            circleModelData.uv = new float[2 * n];
            circleModelData.indicesCount = n * 2;
            circleModelData.verticesCount = n;
        }

        for (int i = 0; i < n; i++)
        {
            circleModelData.indices[i * 2] = i;
            circleModelData.indices[i * 2 + 1] = (i + 1) % (n);
        }
        for (int i = 0; i < n; i++)
        {
            angle = (i * 2 * Game.GetPi() / n);
            circleModelData.xyz[i * 3 + 0] = x + (platform.MathCos(angle) * radius);
            circleModelData.xyz[i * 3 + 1] = y + (platform.MathSin(angle) * radius);
            circleModelData.xyz[i * 3 + 2] = 0;
        }
        for (int i = 0; i < 4 * n; i++)
        {
            circleModelData.rgba[i] = 255;
        }
        for (int i = 0; i < 2 * n; i++)
        {
            circleModelData.uv[i] = 0;
        }

        DrawModelData(circleModelData);

        GLPopMatrix();
    }

    internal int totaltimeMilliseconds;

    internal Entity[] entities;
    internal int entitiesCount;
    internal const int entitiesMax = 4096;
    public const int entityMonsterIdStart = 128;
    public const int entityMonsterIdCount = 128;
    public const int entityLocalIdStart = 256;

    internal void EntityAddLocal(Entity entity)
    {
        for (int i = entityLocalIdStart; i < entitiesCount; i++)
        {
            if (entities[i] == null)
            {
                entities[i] = entity;
                return;
            }
        }
        entities[entitiesCount++] = entity;
    }

    internal float PlayerPushDistance;

    internal Entity CreateBulletEntity(float fromX, float fromY, float fromZ, float toX, float toY, float toZ, float speed)
    {
        Entity entity = new Entity();

        Bullet_ bullet = new Bullet_();
        bullet.fromX = fromX;
        bullet.fromY = fromY;
        bullet.fromZ = fromZ;
        bullet.toX = toX;
        bullet.toY = toY;
        bullet.toZ = toZ;
        bullet.speed = speed;
        entity.bullet = bullet;

        entity.sprite = new Sprite();
        entity.sprite.image = "Sponge.png";
        entity.sprite.size = 4;
        entity.sprite.animationcount = 0;

        return entity;
    }

    public bool Vec3Equal(float ax, float ay, float az, float bx, float by, float bz)
    {
        return ax == bx && ay == by && az == bz;
    }

    internal bool[] keyboardState;

    public const int KeyAltLeft = 5;
    public const int KeyAltRight = 6;

    internal bool Swimming()
    {
        int eyesBlock = GetPlayerEyesBlock();
        if (eyesBlock == -1) { return true; }
        return d_Data.WalkableType1()[eyesBlock] == Packet_WalkableTypeEnum.Fluid;
    }

    internal bool WaterSwimming()
    {
        if (GetPlayerEyesBlock() == -1) { return true; }
        return IsWater(GetPlayerEyesBlock());
    }

    internal bool LavaSwimming()
    {
        return IsLava(GetPlayerEyesBlock());
    }

    internal int GetPlayerEyesBlock()
    {
        float pX = player.position.x;
        float pY = player.position.y;
        float pZ = player.position.z;
        pY += entities[LocalPlayerId].drawModel.eyeHeight;
        int bx = MathFloor(pX);
        int by = MathFloor(pZ);
        int bz = MathFloor(pY);

        if (!IsValidPos(bx, by, bz))
        {
            if (pY < WaterLevel())
            {
                return -1;
            }
            return 0;
        }
        return GetBlockValid(bx, by, bz);
    }

    public float WaterLevel() { return MapSizeZ / 2; }

    internal bool IsLava(int blockType)
    {
        return platform.StringContains(blocktypes[blockType].Name, "Lava"); // todo
    }

    internal int terraincolor()
    {
        if (WaterSwimming())
        {
            return Game.ColorFromArgb(255, 78, 95, 140);
        }
        else if (LavaSwimming())
        {
            return Game.ColorFromArgb(255, 222, 101, 46);
        }
        else
        {
            return Game.ColorFromArgb(255, 255, 255, 255);
        }
    }

    internal void SetAmbientLight(int color)
    {
        int r = Game.ColorR(color);
        int g = Game.ColorG(color);
        int b = Game.ColorB(color);
        platform.GlLightModelAmbient(r, g, b);
    }

    internal OptionsCi options;

    internal int GetKey(int key)
    {
        if (options == null)
        {
            return key;
        }
        if (options.Keys[key] != 0)
        {
            return options.Keys[key];
        }
        return key;
    }

    internal float MoveSpeedNow()
    {
        float movespeednow = movespeed;
        {
            //walk faster on cobblestone
            int blockunderplayer = BlockUnderPlayer();
            if (blockunderplayer != -1)
            {
                float floorSpeed = d_Data.WalkSpeed()[blockunderplayer];
                if (floorSpeed != 0)
                {
                    movespeednow *= floorSpeed;
                }
            }
        }
        if (keyboardState[GetKey(GlKeys.ShiftLeft)])
        {
            //enable_acceleration = false;
            movespeednow *= one * 2 / 10;
        }
        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item != null && item.ItemClass == Packet_ItemClassEnum.Block)
        {
            float itemSpeed = DeserializeFloat(blocktypes[item.BlockId].WalkSpeedWhenUsedFloat);
            if (itemSpeed != 0)
            {
                movespeednow *= itemSpeed;
            }
            if (IronSights)
            {
                float ironSightsSpeed = DeserializeFloat(blocktypes[item.BlockId].IronSightsMoveSpeedFloat);
                if (ironSightsSpeed != 0)
                {
                    movespeednow *= ironSightsSpeed;
                }
            }
        }
        return movespeednow;
    }

    internal float VectorAngleGet(float qX, float qY, float qZ)
    {
        return (platform.MathAcos(qX / Length(qX, qY, qZ)) * MathSign(qZ));
    }

    int MathSign(float q)
    {
        if (q < 0)
        {
            return -1;
        }
        else if (q == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    internal float Length(float x, float y, float z)
    {
        return platform.MathSqrt(x * x + y * y + z * z);
    }

    internal void HandleMaterialKeys(int eKey)
    {
        if (eKey == GetKey(GlKeys.Number1)) { ActiveMaterial = 0; }
        if (eKey == GetKey(GlKeys.Number2)) { ActiveMaterial = 1; }
        if (eKey == GetKey(GlKeys.Number3)) { ActiveMaterial = 2; }
        if (eKey == GetKey(GlKeys.Number4)) { ActiveMaterial = 3; }
        if (eKey == GetKey(GlKeys.Number5)) { ActiveMaterial = 4; }
        if (eKey == GetKey(GlKeys.Number6)) { ActiveMaterial = 5; }
        if (eKey == GetKey(GlKeys.Number7)) { ActiveMaterial = 6; }
        if (eKey == GetKey(GlKeys.Number8)) { ActiveMaterial = 7; }
        if (eKey == GetKey(GlKeys.Number9)) { ActiveMaterial = 8; }
        if (eKey == GetKey(GlKeys.Number0)) { ActiveMaterial = 9; }
    }

    internal void UseVsync()
    {
        platform.SetVSync((ENABLE_LAG == 1) ? false : true);
    }

    internal void ToggleVsync()
    {
        ENABLE_LAG++;
        ENABLE_LAG = ENABLE_LAG % 3;
        UseVsync();
    }

    internal void GuiStateBackToGame()
    {
        guistate = GuiState.Normal;
        SetFreeMouse(false);
    }

    internal float overheadcameradistance;
    internal float tppcameradistance;
    internal int TPP_CAMERA_DISTANCE_MIN;
    internal int TPP_CAMERA_DISTANCE_MAX;
    internal void MouseWheelChanged(MouseWheelEventArgs e)
    {
        float eDeltaPrecise = e.GetDeltaPrecise();
        if (keyboardState[GetKey(GlKeys.LControl)])
        {
            if (cameratype == CameraType.Overhead)
            {
                overheadcameradistance -= eDeltaPrecise;
                if (overheadcameradistance < TPP_CAMERA_DISTANCE_MIN) { overheadcameradistance = TPP_CAMERA_DISTANCE_MIN; }
                if (overheadcameradistance > TPP_CAMERA_DISTANCE_MAX) { overheadcameradistance = TPP_CAMERA_DISTANCE_MAX; }
            }
            if (cameratype == CameraType.Tpp)
            {
                tppcameradistance -= eDeltaPrecise;
                if (tppcameradistance < TPP_CAMERA_DISTANCE_MIN) { tppcameradistance = TPP_CAMERA_DISTANCE_MIN; }
                if (tppcameradistance > TPP_CAMERA_DISTANCE_MAX) { tppcameradistance = TPP_CAMERA_DISTANCE_MAX; }
            }
        }
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnMouseWheelChanged(this, e);
        }
        if ((guistate != GuiState.Inventory)
            && (!keyboardState[GetKey(GlKeys.LControl)]))
        {
            ActiveMaterial -= platform.FloatToInt(eDeltaPrecise);
            ActiveMaterial = ActiveMaterial % 10;
            while (ActiveMaterial < 0)
            {
                ActiveMaterial += 10;
            }
        }
    }

    internal void Connect(string serverAddress, int port, string username, string auth)
    {
        main.Start();
        main.Connect(serverAddress, port);
        SendPacketClient(ClientPackets.CreateLoginPacket(platform, username, auth));
    }

    internal void Connect_(string serverAddress, int port, string username, string auth, string serverPassword)
    {
        main.Start();
        main.Connect(serverAddress, port);
        SendPacketClient(ClientPackets.CreateLoginPacket_(platform, username, auth, serverPassword));
    }

    internal void RedrawAllBlocks()
    {
        terrainRenderer.RedrawAllBlocks();
    }

    //public const int clearcolorR = 171;
    //public const int clearcolorG = 202;
    //public const int clearcolorB = 228;
    //public const int clearcolorA = 255;
    public const int clearcolorR = 0;
    public const int clearcolorG = 0;
    public const int clearcolorB = 0;
    public const int clearcolorA = 255;

    internal void SetFog()
    {
        if (d_Config3d.viewdistance >= 512)
        {
            return;
        }
        //Density for linear fog
        //float density = 0.3f;
        // use this density for exp2 fog (0.0045f was a bit too much at close ranges)
        float density = one * 25 / 10000; // 0.0025f;

        int fogR;
        int fogG;
        int fogB;
        int fogA;

        if (SkySphereNight && (!terrainRenderer.shadowssimple))
        {
            fogR = 0;
            fogG = 0;
            fogB = 0;
            fogA = 255;
        }
        else
        {
            fogR = clearcolorR;
            fogG = clearcolorG;
            fogB = clearcolorB;
            fogA = clearcolorA;
        }
        platform.GlEnableFog();
        platform.GlHintFogHintNicest();
        //old linear fog
        //GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
        // looks better
        platform.GlFogFogModeExp2();
        platform.GlFogFogColor(fogR, fogG, fogB, fogA);
        platform.GlFogFogDensity(density);
        //Unfortunately not used for exp/exp2 fog
        //float fogsize = 10;
        //if (d_Config3d.viewdistance <= 64)
        //{
        //    fogsize = 5;
        //}
        // //float fogstart = d_Config3d.viewdistance - fogsize + 200;
        //float fogstart = d_Config3d.viewdistance - fogsize;
        //GL.Fog(FogParameter.FogStart, fogstart);
        //GL.Fog(FogParameter.FogEnd, fogstart + fogsize);
    }

    public bool ServerVersionAtLeast(int year, int month, int day)
    {
        if (serverGameVersion == null)
        {
            return true;
        }
        if (VersionToInt(serverGameVersion) < DateToInt(year, month, day))
        {
            return false;
        }
        return true;
    }

    bool IsVersionDate(string version)
    {
        IntRef versionCharsCount = new IntRef();
        int[] versionChars = platform.StringToCharArray(version, versionCharsCount);
        if (versionCharsCount.value >= 10)
        {
            if (versionChars[4] == 45 && versionChars[7] == 45) // '-'
            {
                return true;
            }
        }
        return false;
    }

    int VersionToInt(string version)
    {
        int max = 1000 * 1000 * 1000;
        if (!IsVersionDate(version))
        {
            return max;
        }
        FloatRef year = new FloatRef();
        FloatRef month = new FloatRef();
        FloatRef day = new FloatRef();
        if (platform.FloatTryParse(StringTools.StringSubstring(platform, version, 0, 4), year))
        {
            if (platform.FloatTryParse(StringTools.StringSubstring(platform, version, 5, 2), month))
            {
                if (platform.FloatTryParse(StringTools.StringSubstring(platform, version, 8, 2), day))
                {
                    int year_ = platform.FloatToInt(year.value);
                    int month_ = platform.FloatToInt(month.value);
                    int day_ = platform.FloatToInt(day.value);
                    return year_ * 10000 + month_ * 100 + day_;
                }
            }
        }
        return max;
    }

    int DateToInt(int year, int month, int day)
    {
        return year * 10000 + month * 100 + day;
    }

    internal BlockPosSide Nearest(BlockPosSide[] pick2, int pick2Count, float x, float y, float z)
    {
        float minDist = 1000 * 1000;
        BlockPosSide nearest = null;
        for (int i = 0; i < pick2Count; i++)
        {
            float dist = Dist(pick2[i].blockPos[0], pick2[i].blockPos[1], pick2[i].blockPos[2], x, y, z);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = pick2[i];
            }
        }
        return nearest;
    }

    internal BlockOctreeSearcher s;

    internal Kamera overheadcameraK;

    internal void FillChunk(Chunk destination, int destinationchunksize, int sourcex, int sourcey, int sourcez, int[] source, int sourcechunksizeX, int sourcechunksizeY, int sourcechunksizeZ)
    {
        for (int x = 0; x < destinationchunksize; x++)
        {
            for (int y = 0; y < destinationchunksize; y++)
            {
                for (int z = 0; z < destinationchunksize; z++)
                {
                    //if (x + sourcex < source.GetUpperBound(0) + 1
                    //    && y + sourcey < source.GetUpperBound(1) + 1
                    //    && z + sourcez < source.GetUpperBound(2) + 1)
                    {
                        SetBlockInChunk(destination, Index3d(x, y, z, destinationchunksize, destinationchunksize)
                            , source[Index3d(x + sourcex, y + sourcey, z + sourcez, sourcechunksizeX, sourcechunksizeY)]);
                    }
                }
            }
        }
    }

    internal void SetMapPortion(int x, int y, int z, int[] chunk, int sizeX, int sizeY, int sizeZ)
    {
        int chunksizex = sizeX;
        int chunksizey = sizeY;
        int chunksizez = sizeZ;
        if (chunksizex % chunksize != 0) { platform.ThrowException(""); }
        if (chunksizey % chunksize != 0) { platform.ThrowException(""); }
        if (chunksizez % chunksize != 0) { platform.ThrowException(""); }
        Chunk[] localchunks = new Chunk[(chunksizex / chunksize) * (chunksizey / chunksize) * (chunksizez / chunksize)];
        for (int cx = 0; cx < chunksizex / chunksize; cx++)
        {
            for (int cy = 0; cy < chunksizey / chunksize; cy++)
            {
                for (int cz = 0; cz < chunksizex / chunksize; cz++)
                {
                    localchunks[Index3d(cx, cy, cz, (chunksizex / chunksize), (chunksizey / chunksize))] = GetChunk(x + cx * chunksize, y + cy * chunksize, z + cz * chunksize);
                    FillChunk(localchunks[Index3d(cx, cy, cz, (chunksizex / chunksize), (chunksizey / chunksize))], chunksize, cx * chunksize, cy * chunksize, cz * chunksize, chunk, sizeX, sizeY, sizeZ);
                }
            }
        }
        for (int xxx = 0; xxx < chunksizex; xxx += chunksize)
        {
            for (int yyy = 0; yyy < chunksizex; yyy += chunksize)
            {
                for (int zzz = 0; zzz < chunksizex; zzz += chunksize)
                {
                    terrainRenderer.SetChunkDirty((x + xxx) / chunksize, (y + yyy) / chunksize, (z + zzz) / chunksize, true, true);
                    SetChunksAroundDirty((x + xxx) / chunksize, (y + yyy) / chunksize, (z + zzz) / chunksize);
                }
            }
        }
    }

    internal void ChatLog(string p)
    {
        if (!platform.ChatLog(this.ServerInfo.ServerName, p))
        {
            platform.ConsoleWriteLine(platform.StringFormat(language.CannotWriteChatLog(), this.ServerInfo.ServerName));
        }
    }

    //value is original block.
    internal DictionaryVector3Float fillarea;
    internal Vector3IntRef fillstart;
    internal Vector3IntRef fillend;
    internal int fillAreaLimit;

    internal void ClearFillArea()
    {
        for (int i = 0; i < fillarea.itemsCount; i++)
        {
            Vector3Float k = fillarea.items[i];
            if (k == null)
            {
                continue;
            }
            SetBlock(k.x, k.y, k.z, platform.FloatToInt(k.value));
            RedrawBlock(k.x, k.y, k.z);
        }
        fillarea.Clear();
    }

    internal void FillFill(Vector3IntRef a_, Vector3IntRef b_)
    {
        int startx = Game.MinInt(a_.X, b_.X);
        int endx = Game.MaxInt(a_.X, b_.X);
        int starty = Game.MinInt(a_.Y, b_.Y);
        int endy = Game.MaxInt(a_.Y, b_.Y);
        int startz = Game.MinInt(a_.Z, b_.Z);
        int endz = Game.MaxInt(a_.Z, b_.Z);
        for (int x = startx; x <= endx; x++)
        {
            for (int y = starty; y <= endy; y++)
            {
                for (int z = startz; z <= endz; z++)
                {
                    if (fillarea.Count() > fillAreaLimit)
                    {
                        ClearFillArea();
                        return;
                    }
                    if (!IsFillBlock(GetBlock(x, y, z)))
                    {
                        fillarea.Set(x, y, z, GetBlock(x, y, z));
                        SetBlock(x, y, z, d_Data.BlockIdFillArea());
                        RedrawBlock(x, y, z);
                    }
                }
            }
        }
    }

    internal void OnPickUseWithTool(int posX, int posY, int posZ)
    {
        SendSetBlock(posX, posY, posZ, Packet_BlockSetModeEnum.UseWithTool, d_Inventory.RightHand[ActiveMaterial].BlockId, ActiveMaterial);
    }

    internal void KeyUp(int eKey)
    {
        for (int i = 0; i < clientmodsCount; i++)
        {
            KeyEventArgs args_ = new KeyEventArgs();
            args_.SetKeyCode(eKey);
            clientmods[i].OnKeyUp(this, args_);
            if (args_.GetHandled())
            {
                return;
            }
        }
        keyboardState[eKey] = false;
        if (eKey == GetKey(GlKeys.ShiftLeft) || eKey == GetKey(GlKeys.ShiftRight))
        {
            IsShiftPressed = false;
        }
    }
    internal float playerPositionSpawnX;
    internal float playerPositionSpawnY;
    internal float playerPositionSpawnZ;

    internal void MapLoaded()
    {
        terrainRenderer.StartTerrain();
        RedrawAllBlocks();
        materialSlots = d_Data.DefaultMaterialSlots();
        GuiStateBackToGame();

        playerPositionSpawnX = player.position.x;
        playerPositionSpawnY = player.position.y;
        playerPositionSpawnZ = player.position.z;
    }
    internal int[] materialSlots;

    internal void Draw2dText1(string text, int x, int y, int fontsize, IntRef color, bool enabledepthtest)
    {
        FontCi font = new FontCi();
        font.family = "Arial";
        font.size = fontsize;
        Draw2dText(text, font, x, y, color, enabledepthtest);
    }

    internal InventoryUtilClient d_InventoryUtil;
    internal void UseInventory(Packet_Inventory packet_Inventory)
    {
        d_Inventory = packet_Inventory;
        d_InventoryUtil.d_Inventory = packet_Inventory;
    }

    internal void KeyPress(int eKeyChar)
    {
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            KeyPressEventArgs args_ = new KeyPressEventArgs();
            args_.SetKeyChar(eKeyChar);
            clientmods[i].OnKeyPress(this, args_);
            if (args_.GetHandled())
            {
                return;
            }
        }
    }

    public string CharToString(int c)
    {
        int[] arr = new int[1];
        arr[0] = c;
        return platform.CharArrayToString(arr, 1);
    }

    internal Speculative[] speculative;
    internal int speculativeCount;
    internal const int speculativeMax = 8 * 1024;

    internal void SendSetBlockAndUpdateSpeculative(int material, int x, int y, int z, int mode)
    {
        SendSetBlock(x, y, z, mode, material, ActiveMaterial);

        Packet_Item item = d_Inventory.RightHand[ActiveMaterial];
        if (item != null && item.ItemClass == Packet_ItemClassEnum.Block)
        {
            //int blockid = d_Inventory.RightHand[d_Viewport.ActiveMaterial].BlockId;
            int blockid = material;
            if (mode == Packet_BlockSetModeEnum.Destroy)
            {
                blockid = SpecialBlockId.Empty;
            }
            Speculative s_ = new Speculative();
            s_.x = x;
            s_.y = y;
            s_.z = z;
            s_.blocktype = GetBlock(x, y, z);
            s_.timeMilliseconds = platform.TimeMillisecondsFromStart();
            AddSpeculative(s_);
            SetBlock(x, y, z, blockid);
            RedrawBlock(x, y, z);
        }
        else
        {
            //TODO
        }
    }

    void AddSpeculative(Speculative s_)
    {
        for (int i = 0; i < speculativeCount; i++)
        {
            if (speculative[i] == null)
            {
                speculative[i] = s_;
                return;
            }
        }
        speculative[speculativeCount++] = s_;
    }

    internal void OnNewFrame(float dt)
    {
        for (int i = 0; i < speculativeCount; i++)
        {
            Speculative s_ = speculative[i];
            if (s_ == null)
            {
                continue;
            }
            if ((one * (platform.TimeMillisecondsFromStart() - s_.timeMilliseconds) / 1000) > 2)
            {
                RedrawBlock(s_.x, s_.y, s_.z);
                speculative[i] = null;
            }
        }
    }

    internal void OnPick(int blockposX, int blockposY, int blockposZ, int blockposoldX, int blockposoldY, int blockposoldZ, float[] collisionPos, bool right)
    {
        float xfract = collisionPos[0] - MathFloor(collisionPos[0]);
        float zfract = collisionPos[2] - MathFloor(collisionPos[2]);
        int activematerial = MaterialSlots(ActiveMaterial);
        int railstart = d_Data.BlockIdRailstart();
        if (activematerial == railstart + RailDirectionFlags.TwoHorizontalVertical
            || activematerial == railstart + RailDirectionFlags.Corners)
        {
            RailDirection dirnew;
            if (activematerial == railstart + RailDirectionFlags.TwoHorizontalVertical)
            {
                dirnew = PickHorizontalVertical(xfract, zfract);
            }
            else
            {
                dirnew = PickCorners(xfract, zfract);
            }
            int dir = d_Data.Rail()[GetBlock(blockposoldX, blockposoldY, blockposoldZ)];
            if (dir != 0)
            {
                blockposX = blockposoldX;
                blockposY = blockposoldY;
                blockposZ = blockposoldZ;
            }
            activematerial = railstart + (dir | DirectionUtils.ToRailDirectionFlags(dirnew));
            //Console.WriteLine(blockposold);
            //Console.WriteLine(xfract + ":" + zfract + ":" + activematerial + ":" + dirnew);
        }
        int x = platform.FloatToInt(blockposX);
        int y = platform.FloatToInt(blockposY);
        int z = platform.FloatToInt(blockposZ);
        int mode = right ? Packet_BlockSetModeEnum.Create : Packet_BlockSetModeEnum.Destroy;
        {
            if (IsAnyPlayerInPos(x, y, z) || activematerial == 151)
            {
                return;
            }
            Vector3IntRef v = Vector3IntRef.Create(x, y, z);
            Vector3IntRef oldfillstart = fillstart;
            Vector3IntRef oldfillend = fillend;
            if (mode == Packet_BlockSetModeEnum.Create)
            {
                if (blocktypes[activematerial].IsTool)
                {
                    OnPickUseWithTool(blockposX, blockposY, blockposZ);
                    return;
                }

                //if (GameDataManicDigger.IsDoorTile(activematerial))
                //{
                //    if (z + 1 == d_Map.MapSizeZ || z == 0) return;
                //}

                if (activematerial == d_Data.BlockIdCuboid())
                {
                    ClearFillArea();

                    if (fillstart != null)
                    {
                        Vector3IntRef f = fillstart;
                        if (!IsFillBlock(GetBlock(f.X, f.Y, f.Z)))
                        {
                            fillarea.Set(f.X, f.Y, f.Z, GetBlock(f.X, f.Y, f.Z));
                        }
                        SetBlock(f.X, f.Y, f.Z, d_Data.BlockIdFillStart());


                        FillFill(v, fillstart);
                    }
                    if (!IsFillBlock(GetBlock(v.X, v.Y, v.Z)))
                    {
                        fillarea.Set(v.X, v.Y, v.Z, GetBlock(v.X, v.Y, v.Z));
                    }
                    SetBlock(v.X, v.Y, v.Z, d_Data.BlockIdCuboid());
                    fillend = v;
                    RedrawBlock(v.X, v.Y, v.Z);
                    return;
                }
                if (activematerial == d_Data.BlockIdFillStart())
                {
                    ClearFillArea();
                    if (!IsFillBlock(GetBlock(v.X, v.Y, v.Z)))
                    {
                        fillarea.Set(v.X, v.Y, v.Z, GetBlock(v.X, v.Y, v.Z));
                    }
                    SetBlock(v.X, v.Y, v.Z, d_Data.BlockIdFillStart());
                    fillstart = v;
                    fillend = null;
                    RedrawBlock(v.X, v.Y, v.Z);
                    return;
                }
                if (fillarea.ContainsKey(v.X, v.Y, v.Z))// && fillarea[v])
                {
                    SendFillArea(fillstart.X, fillstart.Y, fillstart.Z, fillend.X, fillend.Y, fillend.Z, activematerial);
                    ClearFillArea();
                    fillstart = null;
                    fillend = null;
                    return;
                }
            }
            else
            {
                if (blocktypes[activematerial].IsTool)
                {
                    OnPickUseWithTool(blockposX, blockposY, blockposoldZ);
                    return;
                }
                //delete fill start
                if (fillstart != null && fillstart.X == v.X && fillstart.Y == v.Y && fillstart.Z == v.Z)
                {
                    ClearFillArea();
                    fillstart = null;
                    fillend = null;
                    return;
                }
                //delete fill end
                if (fillend != null && fillend.X == v.X && fillend.Y == v.Y && fillend.Z == v.Z)
                {
                    ClearFillArea();
                    fillend = null;
                    return;
                }
            }
            if (mode == Packet_BlockSetModeEnum.Create && activematerial == d_Data.BlockIdMinecart())
            {
                //CommandRailVehicleBuild cmd2 = new CommandRailVehicleBuild();
                //cmd2.x = (short)x;
                //cmd2.y = (short)y;
                //cmd2.z = (short)z;
                //TrySendCommand(MakeCommand(CommandId.RailVehicleBuild, cmd2));
                return;
            }
            //if (TrySendCommand(MakeCommand(CommandId.Build, cmd)))
            SendSetBlockAndUpdateSpeculative(activematerial, x, y, z, mode);
        }
    }

    internal void Set3dProjection1(float zfar_)
    {
        Set3dProjection(zfar_, currentfov());
    }

    internal void Set3dProjection2()
    {
        Set3dProjection1(zfar());
    }

    internal void SendGameResolution()
    {
        SendPacketClient(ClientPackets.GameResolution(Width(), Height()));
    }

    bool sendResize;
    internal void OnResize()
    {
        platform.GlViewport(0, 0, Width(), Height());
        this.Set3dProjection2();
        //Notify server of size change
        if (sendResize)
        {
            SendGameResolution();
        }
    }

    internal void Reconnect()
    {
        reconnect = true;
    }

    internal Packet_ServerRedirect redirectTo;
    internal void ExitAndSwitchServer(Packet_ServerRedirect newServer)
    {
        if (issingleplayer)
        {
            platform.SinglePlayerServerExit();
        }
        redirectTo = newServer;
        exitToMainMenu = true;
    }
    
    internal Packet_ServerRedirect GetRedirect()
    {
        return redirectTo;
    }

    internal void ExitToMainMenu_()
    {
        if (issingleplayer)
        {
            platform.SinglePlayerServerExit();
        }
        redirectTo = null;
        exitToMainMenu = true;
    }

    internal void ClientCommand(string s_)
    {
        if (s_ == "")
        {
            return;
        }
        IntRef ssCount = new IntRef();
        string[] ss = platform.StringSplit(s_, " ", ssCount);
        if (StringTools.StringStartsWith(platform, s_, "."))
        {
            string strFreemoveNotAllowed = language.FreemoveNotAllowed();
            //try
            {
                string cmd = StringTools.StringSubstringToEnd(platform, ss[0], 1);
                string arguments;
                if (platform.StringIndexOf(s_, " ") == -1)
                {
                    arguments = "";
                }
                else
                {
                    arguments = StringTools.StringSubstringToEnd(platform, s_, platform.StringIndexOf(s_, " "));
                }
                arguments = platform.StringTrim(arguments);
                if (cmd == "pos")
                {
                    ENABLE_DRAWPOSITION = BoolCommandArgument(arguments);
                }
                else if (cmd == "fog")
                {
                    int foglevel;
                    foglevel = platform.IntParse(arguments);
                    //if (foglevel <= 16)
                    //{
                    //    terrain.DrawDistance = (int)Math.Pow(2, foglevel);
                    //}
                    //else
                    {
                        int foglevel2 = foglevel;
                        if (foglevel2 > 1024)
                        {
                            foglevel2 = 1024;
                        }
                        if (foglevel2 % 2 == 0)
                        {
                            foglevel2--;
                        }
                        d_Config3d.viewdistance = foglevel2;
                        //terrain.UpdateAllTiles();
                    }
                    OnResize();
                }
                else if (cmd == "noclip")
                {
                    ENABLE_NOCLIP = BoolCommandArgument(arguments);
                }
                else if (cmd == "freemove")
                {
                    if (this.AllowFreemove)
                    {
                        ENABLE_FREEMOVE = BoolCommandArgument(arguments);
                    }
                    else
                    {
                        Log(strFreemoveNotAllowed);
                        return;
                    }
                }
                else if (cmd == "fov")
                {
                    int arg = platform.IntParse(arguments);
                    int minfov = 1;
                    int maxfov = 179;
                    if (!issingleplayer)
                    {
                        minfov = 60;
                    }
                    if (arg < minfov || arg > maxfov)
                    {
                        platform.ThrowException(platform.StringFormat2("Valid field of view: {0}-{1}", platform.IntToString(minfov), platform.IntToString(maxfov)));
                    }
                    float fov_ = (2 * Game.GetPi() * (one * arg / 360));
                    this.fov = fov_;
                    OnResize();
                }
                else if (cmd == "clients")
                {
                    Log("Clients:");
                    for (int i = 0; i < entitiesCount; i++)
                    {
                        Entity entity = entities[i];
                        if (entity == null) { continue; }
                        if (entity.drawName == null) { continue; }
                        if (!entity.drawName.ClientAutoComplete) { continue; }
                        Log(platform.StringFormat2("{0} {1}", platform.IntToString(i), entities[i].drawName.Name));
                    }
                }
                else if (cmd == "movespeed")
                {
                    //try
                    //{
                    if (this.AllowFreemove)
                    {
                        if (platform.FloatParse(arguments) <= 500)
                        {
                            movespeed = basemovespeed * platform.FloatParse(arguments);
                            AddChatline(platform.StringFormat("Movespeed: {0}x", arguments));
                        }
                        else
                        {
                            AddChatline("Entered movespeed to high! max. 500x");
                        }
                    }
                    else
                    {
                        Log(strFreemoveNotAllowed);
                        return;
                    }
                    //}
                    //catch
                    //{
                    //    AddChatline("Invalid value!");
                    //    AddChatline("USE: .movespeed [movespeed]");
                    //}
                }
                else if (cmd == "gui")
                {
                    ENABLE_DRAW2D = BoolCommandArgument(arguments);
                }
                else if (cmd == "reconnect")
                {
                    Reconnect();
                }
                else if (cmd == "serverinfo")
                {
                    //Fetches server info from given adress
                    IntRef splitCount = new IntRef();
                    string[] split = platform.StringSplit(arguments, ":", splitCount);
                    if (splitCount.value == 2)
                    {
                        QueryClient qClient = new QueryClient();
                        qClient.SetPlatform(platform);
                        qClient.PerformQuery(split[0], platform.IntParse(split[1]));
                        if (qClient.querySuccess)
                        {
                            //Received result
                            QueryResult r = qClient.GetResult();
                            AddChatline(r.GameMode);
                            AddChatline(platform.IntToString(r.MapSizeX));
                            AddChatline(platform.IntToString(r.MapSizeY));
                            AddChatline(platform.IntToString(r.MapSizeZ));
                            AddChatline(platform.IntToString(r.MaxPlayers));
                            AddChatline(r.MOTD);
                            AddChatline(r.Name);
                            AddChatline(platform.IntToString(r.PlayerCount));
                            AddChatline(r.PlayerList);
                            AddChatline(platform.IntToString(r.Port));
                            AddChatline(r.PublicHash);
                            AddChatline(r.ServerVersion);
                        }
                        AddChatline(qClient.GetServerMessage());
                    }
                }
                else
                {
                    for (int i = 0; i < clientmodsCount; i++)
                    {
                        ClientCommandArgs args = new ClientCommandArgs();
                        args.arguments = arguments;
                        args.command = cmd;
                        clientmods[i].OnClientCommand(this, args);
                    }
                    string chatline = StringTools.StringSubstring(platform, GuiTypingBuffer, 0, MinInt(GuiTypingBuffer.Length, 256));
                    SendChat(chatline);
                }
            }
            //catch (Exception e) { AddChatline(new StringReader(e.Message).ReadLine()); }
        }
        else
        {
            string chatline = StringTools.StringSubstring(platform, GuiTypingBuffer, 0, Game.MinInt(StringTools.StringLength(platform, GuiTypingBuffer), 4096));
            SendChat(chatline);
        }
    }
    public bool BoolCommandArgument(string arguments)
    {
        arguments = platform.StringTrim(arguments);
        return (arguments == "" || arguments == "1" || arguments == "on" || arguments == "yes");
    }
    internal string[] typinglog;
    internal int typinglogCount;

    string[] getAsset;
    internal void ProcessServerIdentification(Packet_Server packet)
    {
        this.LocalPlayerId = packet.Identification.AssignedClientId;
        this.ServerInfo.connectdata = this.connectdata;
        this.ServerInfo.ServerName = packet.Identification.ServerName;
        this.ServerInfo.ServerMotd = packet.Identification.ServerMotd;
        this.d_TerrainChunkTesselator.ENABLE_TEXTURE_TILING = packet.Identification.RenderHint_ == RenderHintEnum.Fast;
        Packet_StringList requiredMd5 = packet.Identification.RequiredBlobMd5;
        Packet_StringList requiredName = packet.Identification.RequiredBlobName;
        ChatLog("[GAME] Processed server identification");
        int getCount = 0;
        if (requiredMd5 != null)
        {
            ChatLog(platform.StringFormat("[GAME] Server has {0} assets", platform.IntToString(requiredMd5.ItemsCount)));
            for (int i = 0; i < requiredMd5.ItemsCount; i++)
            {
                string md5 = requiredMd5.Items[i];
                
                //check if file with that content is already in cache
                if (platform.IsCached(md5))
                {
                    //File has been cached. load cached version.
                    Asset cachedAsset = platform.LoadAssetFromCache(md5);
                    string name;
                    if (requiredName != null)
                    {
                        name = requiredName.Items[i];
                    }
                    else // server older than 2014-07-13.
                    {
                        name = cachedAsset.name;
                    }
                    SetFile(name, cachedAsset.md5, cachedAsset.data, cachedAsset.dataLength);
                }
                else
                {
                    //Asset not present in cache
                    if (requiredName != null)
                    {
                        //If list of names is given (server > 2014-07-13) lookup if asset is already loaded
                        if (!HasAsset(md5, requiredName.Items[i]))
                        {
                            //Request asset from server if not already loaded
                            getAsset[getCount++] = md5;
                        }
                    }
                    else
                    {
                        //Server didn't send list of required asset names
                        getAsset[getCount++] = md5;
                    }
                }
            }
            ChatLog(platform.StringFormat("[GAME] Will download {0} missing assets", platform.IntToString(getCount)));
        }
        SendGameResolution();
        ChatLog("[GAME] Sent window resolution to server");
        sendResize = true;
        SendRequestBlob(getAsset, getCount);
        ChatLog("[GAME] Sent BLOB request");
        if (packet.Identification.MapSizeX != MapSizeX
            || packet.Identification.MapSizeY != MapSizeY
            || packet.Identification.MapSizeZ != MapSizeZ)
        {
            Reset(packet.Identification.MapSizeX,
                packet.Identification.MapSizeY,
                packet.Identification.MapSizeZ);
            d_Heightmap.Restart();
        }
        terrainRenderer.shadowssimple = packet.Identification.DisableShadows == 1 ? true : false;
        maxdrawdistance = packet.Identification.PlayerAreaSize / 2;
        if (maxdrawdistance == 0)
        {
            maxdrawdistance = 128;
        }
        ChatLog("[GAME] Map initialized");
    }

    bool HasAsset(string md5, string name)
    {
        for (int i = 0; i < assets.count; i++)
        {
            if (assets.items[i].md5 == md5)
            {
                if (assets.items[i].name == name)
                {
                    //Check both MD5 and name as there might be files with same content
                    return true;
                }
            }
        }
        return false;
    }

    internal bool handRedraw;
    internal bool handSetAttackBuild;
    internal bool handSetAttackDestroy;

    string serverGameVersion;
    internal void ProcessPacket(Packet_Server packet)
    {
        if (packetHandlers[packet.Id] != null)
        {
            packetHandlers[packet.Id].Handle(this, packet);
        }
        switch (packet.Id)
        {
            case Packet_ServerIdEnum.ServerIdentification:
                {
                    string invalidversionstr = language.InvalidVersionConnectAnyway();

                    serverGameVersion = packet.Identification.MdProtocolVersion;
                    if (serverGameVersion != platform.GetGameVersion())
                    {
                        ChatLog("[GAME] Different game versions");
                        string q = platform.StringFormat2(invalidversionstr, platform.GetGameVersion(), serverGameVersion);
                        invalidVersionDrawMessage = q;
                        invalidVersionPacketIdentification = packet;
                    }
                    else
                    {
                        ProcessServerIdentification(packet);
                    }
                    ReceivedMapLength = 0;
                }
                break;
            case Packet_ServerIdEnum.Ping:
                {
                    this.SendPingReply();
                    this.ServerInfo.ServerPing.Send(platform);
                }
                break;
            case Packet_ServerIdEnum.PlayerPing:
                {
                    this.ServerInfo.ServerPing.Receive(platform);
                }
                break;
            case Packet_ServerIdEnum.LevelInitialize:
                {
                    ChatLog("[GAME] Initialized map loading");
                    ReceivedMapLength = 0;
                    InvokeMapLoadingProgress(0, 0, language.Connecting());
                }
                break;
            case Packet_ServerIdEnum.LevelDataChunk:
                {
                    MapLoadingPercentComplete = packet.LevelDataChunk.PercentComplete;
                    MapLoadingStatus = packet.LevelDataChunk.Status;
                    InvokeMapLoadingProgress(MapLoadingPercentComplete, ReceivedMapLength, MapLoadingStatus);
                }
                break;
            case Packet_ServerIdEnum.LevelFinalize:
                {
                    ChatLog("[GAME] Finished map loading");
                    //d_Data.Load(MyStream.ReadAllLines(d_GetFile.GetFile("blocks.csv")),
                    //    MyStream.ReadAllLines(d_GetFile.GetFile("defaultmaterialslots.csv")),
                    //    MyStream.ReadAllLines(d_GetFile.GetFile("lightlevels.csv")));
                    //d_CraftingRecipes.Load(MyStream.ReadAllLines(d_GetFile.GetFile("craftingrecipes.csv")));

                    //MapLoaded();
                }
                break;
            case Packet_ServerIdEnum.SetBlock:
                {
                    int x = packet.SetBlock.X;
                    int y = packet.SetBlock.Y;
                    int z = packet.SetBlock.Z;
                    int type = packet.SetBlock.BlockType;
                    //try
                    {
                        SetTileAndUpdate(x, y, z, type);
                    }
                    //catch { Console.WriteLine("Cannot update tile!"); }
                }
                break;
            case Packet_ServerIdEnum.FillArea:
                {
                    int ax = packet.FillArea.X1;
                    int ay = packet.FillArea.Y1;
                    int az = packet.FillArea.Z1;
                    int bx = packet.FillArea.X2;
                    int by = packet.FillArea.Y2;
                    int bz = packet.FillArea.Z2;

                    int startx = MinInt(ax, bx);
                    int endx = MaxInt(ax, bx);
                    int starty = MinInt(ay, by);
                    int endy = MaxInt(ay, by);
                    int startz = MinInt(az, bz);
                    int endz = MaxInt(az, bz);

                    int blockCount = packet.FillArea.BlockCount;
                    {
                        for (int x = startx; x <= endx; x++)
                        {
                            for (int y = starty; y <= endy; y++)
                            {
                                for (int z = startz; z <= endz; z++)
                                {
                                    // if creative mode is off and player run out of blocks
                                    if (blockCount == 0)
                                    {
                                        return;
                                    }
                                    //try
                                    {
                                        SetTileAndUpdate(x, y, z, packet.FillArea.BlockType);
                                    }
                                    //catch
                                    //{
                                    //    Console.WriteLine("Cannot update tile!");
                                    //}
                                    blockCount--;
                                }
                            }
                        }
                    }
                }
                break;
            case Packet_ServerIdEnum.FillAreaLimit:
                {
                    this.fillAreaLimit = packet.FillAreaLimit.Limit;
                    if (this.fillAreaLimit > 100000)
                    {
                        this.fillAreaLimit = 100000;
                    }
                }
                break;
            case Packet_ServerIdEnum.Freemove:
                {
                    this.AllowFreemove = packet.Freemove.IsEnabled != 0;
                    if (!this.AllowFreemove)
                    {
                        ENABLE_FREEMOVE = false;
                        ENABLE_NOCLIP = false;
                        movespeed = basemovespeed;
                        Log(language.MoveNormal());
                    }
                }
                break;
            case Packet_ServerIdEnum.PlayerSpawnPosition:
                {
                    int x = packet.PlayerSpawnPosition.X;
                    int y = packet.PlayerSpawnPosition.Y;
                    int z = packet.PlayerSpawnPosition.Z;
                    this.playerPositionSpawnX = x;
                    this.playerPositionSpawnY = z;
                    this.playerPositionSpawnZ = y;
                    Log(platform.StringFormat(language.SpawnPositionSetTo(), platform.StringFormat3("{0},{1},{2}", platform.IntToString(x), platform.IntToString(y), platform.IntToString(z))));
                }
                break;
            case Packet_ServerIdEnum.Message:
                {
                    AddChatline(packet.Message.Message);
                    ChatLog(packet.Message.Message);
                }
                break;
            case Packet_ServerIdEnum.DisconnectPlayer:
                {
                    ChatLog(platform.StringFormat("[GAME] Disconnected by the server ({0})", packet.DisconnectPlayer.DisconnectReason));
                    //When server disconnects player, return to main menu
                    platform.MessageBoxShowError(packet.DisconnectPlayer.DisconnectReason, "Disconnected from server");
                    ExitToMainMenu_();
                    break;
                }
            case Packet_ServerIdEnum.PlayerStats:
                {
                    Packet_ServerPlayerStats p = packet.PlayerStats;
                    this.PlayerStats = p;
                }
                break;
            case Packet_ServerIdEnum.FiniteInventory:
                {
                    //check for null so it's possible to connect
                    //to old versions of game (before 2011-05-05)
                    if (packet.Inventory.Inventory != null)
                    {
                        //d_Inventory.CopyFrom(ConvertInventory(packet.Inventory.Inventory));
                        UseInventory(packet.Inventory.Inventory);
                    }
                    //FiniteInventory = packet.FiniteInventory.BlockTypeAmount;
                    //ENABLE_FINITEINVENTORY = packet.FiniteInventory.IsFinite;
                    //FiniteInventoryMax = packet.FiniteInventory.Max;
                }
                break;
            case Packet_ServerIdEnum.Season:
                {
                    packet.Season.Hour -= 1;
                    if (packet.Season.Hour < 0)
                    {
                        //shouldn't happen
                        packet.Season.Hour = 12 * HourDetail;
                    }
                    int sunlight = NightLevels[packet.Season.Hour];
                    SkySphereNight = sunlight < 8;
                    d_SunMoonRenderer.day_length_in_seconds = 60 * 60 * 24 / packet.Season.DayNightCycleSpeedup;
                    int hour = packet.Season.Hour / HourDetail;
                    if (d_SunMoonRenderer.GetHour() != hour)
                    {
                        d_SunMoonRenderer.SetHour(hour);
                    }

                    if (sunlight_ != sunlight)
                    {
                        sunlight_ = sunlight;
                        //d_Shadows.ResetShadows();
                        RedrawAllBlocks();
                    }
                }
                break;
            case Packet_ServerIdEnum.BlobInitialize:
                {
                    blobdownload = new CitoMemoryStream();
                    //blobdownloadhash = ByteArrayToString(packet.BlobInitialize.hash);
                    blobdownloadname = packet.BlobInitialize.Name;
                    blobdownloadmd5 = packet.BlobInitialize.Md5;
                }
                break;
            case Packet_ServerIdEnum.BlobPart:
                {
                    int length = platform.ByteArrayLength(packet.BlobPart.Data);
                    blobdownload.Write(packet.BlobPart.Data, 0, length);
                    ReceivedMapLength += length;
                }
                break;
            case Packet_ServerIdEnum.BlobFinalize:
                {
                    byte[] downloaded = blobdownload.ToArray();

                    if (blobdownloadname != null) // old servers
                    {
                        SetFile(blobdownloadname, blobdownloadmd5, downloaded, blobdownload.Length());
                    }
                    blobdownload = null;
                }
                break;
            case Packet_ServerIdEnum.Sound:
                {
                    PlaySoundAt(packet.Sound.Name, packet.Sound.X, packet.Sound.Y, packet.Sound.Z);
                }
                break;
            case Packet_ServerIdEnum.RemoveMonsters:
                {
                    for (int i = entityMonsterIdStart; i < entityMonsterIdStart + entityMonsterIdCount; i++)
                    {
                        entities[i] = null;
                    }
                }
                break;
            case Packet_ServerIdEnum.Translation:
                language.Override(packet.Translation.Lang, packet.Translation.Id, packet.Translation.Translation);
                break;
            case Packet_ServerIdEnum.BlockType:
                NewBlockTypes[packet.BlockType.Id] = packet.BlockType.Blocktype;
                break;
            case Packet_ServerIdEnum.SunLevels:
                NightLevels = packet.SunLevels.Sunlevels;
                break;
            case Packet_ServerIdEnum.LightLevels:
                for (int i = 0; i < packet.LightLevels.LightlevelsCount; i++)
                {
                    mLightLevels[i] = DeserializeFloat(packet.LightLevels.Lightlevels[i]);
                }
                break;
            case Packet_ServerIdEnum.Follow:
                IntRef oldFollowId = FollowId();
                Follow = packet.Follow.Client;
                if (packet.Follow.Tpp != 0)
                {
                    SetCamera(CameraType.Overhead);
                    player.position.rotx = Game.GetPi();
                    GuiStateBackToGame();
                }
                else
                {
                    SetCamera(CameraType.Fpp);
                }
                break;
            case Packet_ServerIdEnum.Bullet:
                EntityAddLocal(CreateBulletEntity(
                    DeserializeFloat(packet.Bullet.FromXFloat),
                    DeserializeFloat(packet.Bullet.FromYFloat),
                    DeserializeFloat(packet.Bullet.FromZFloat),
                    DeserializeFloat(packet.Bullet.ToXFloat),
                    DeserializeFloat(packet.Bullet.ToYFloat),
                    DeserializeFloat(packet.Bullet.ToZFloat),
                    DeserializeFloat(packet.Bullet.SpeedFloat)));
                break;
            case Packet_ServerIdEnum.Ammo:
                if (!ammostarted)
                {
                    ammostarted = true;
                    for (int i = 0; i < packet.Ammo.TotalAmmoCount; i++)
                    {
                        Packet_IntInt k = packet.Ammo.TotalAmmo[i];
                        LoadedAmmo[k.Key_] = MinInt(k.Value_, blocktypes[k.Key_].AmmoMagazine);
                    }
                }
                TotalAmmo = new int[GlobalVar.MAX_BLOCKTYPES];
                for (int i = 0; i < packet.Ammo.TotalAmmoCount; i++)
                {
                    TotalAmmo[packet.Ammo.TotalAmmo[i].Key_] = packet.Ammo.TotalAmmo[i].Value_;
                }
                break;
            case Packet_ServerIdEnum.Explosion:
                {
                    Entity entity = new Entity();
                    entity.expires = new Expires();
                    entity.expires.timeLeft = DeserializeFloat(packet.Explosion.TimeFloat);
                    entity.push = packet.Explosion;
                    EntityAddLocal(entity);
                }
                break;
            case Packet_ServerIdEnum.Projectile:
                {
                    Entity entity = new Entity();

                    Sprite sprite = new Sprite();
                    sprite.image = "ChemicalGreen.png";
                    sprite.size = 14;
                    sprite.animationcount = 0;
                    sprite.positionX = DeserializeFloat(packet.Projectile.FromXFloat);
                    sprite.positionY = DeserializeFloat(packet.Projectile.FromYFloat);
                    sprite.positionZ = DeserializeFloat(packet.Projectile.FromZFloat);
                    entity.sprite = sprite;

                    Grenade_ grenade = new Grenade_();
                    grenade.velocityX = DeserializeFloat(packet.Projectile.VelocityXFloat);
                    grenade.velocityY = DeserializeFloat(packet.Projectile.VelocityYFloat);
                    grenade.velocityZ = DeserializeFloat(packet.Projectile.VelocityZFloat);
                    grenade.block = packet.Projectile.BlockId;
                    grenade.sourcePlayer = packet.Projectile.SourcePlayerID;
                    entity.grenade = grenade;

                    entity.expires = Expires.Create(DeserializeFloat(packet.Projectile.ExplodesAfterFloat));

                    EntityAddLocal(entity);
                }
                break;
            case Packet_ServerIdEnum.BlockTypes:
                blocktypes = NewBlockTypes;
                NewBlockTypes = new Packet_BlockType[GlobalVar.MAX_BLOCKTYPES];

                int textureInAtlasIdsCount = 1024;
                string[] textureInAtlasIds = new string[textureInAtlasIdsCount];
                int lastTextureId = 0;
                for (int i = 0; i < GlobalVar.MAX_BLOCKTYPES; i++)
                {
                    if (blocktypes[i] != null)
                    {
                        string[] to_load = new string[7];
                        int to_loadLength = 7;
                        {
                            to_load[0] = blocktypes[i].TextureIdLeft;
                            to_load[1] = blocktypes[i].TextureIdRight;
                            to_load[2] = blocktypes[i].TextureIdFront;
                            to_load[3] = blocktypes[i].TextureIdBack;
                            to_load[4] = blocktypes[i].TextureIdTop;
                            to_load[5] = blocktypes[i].TextureIdBottom;
                            to_load[6] = blocktypes[i].TextureIdForInventory;
                        }
                        for (int k = 0; k < to_loadLength; k++)
                        {
                            if (!Contains(textureInAtlasIds, textureInAtlasIdsCount, to_load[k]))
                            {
                                textureInAtlasIds[lastTextureId++] = to_load[k];
                            }
                        }
                    }
                }
                d_Data.UseBlockTypes(platform, blocktypes, GlobalVar.MAX_BLOCKTYPES);
                for (int i = 0; i < GlobalVar.MAX_BLOCKTYPES; i++)
                {
                    Packet_BlockType b = blocktypes[i];
                    if (b == null)
                    {
                        continue;
                    }
                    //Indexed by block id and TileSide.
                    if (textureInAtlasIds != null)
                    {
                        TextureId[i][0] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdTop);
                        TextureId[i][1] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdBottom);
                        TextureId[i][2] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdFront);
                        TextureId[i][3] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdBack);
                        TextureId[i][4] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdLeft);
                        TextureId[i][5] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdRight);
                        TextureIdForInventory[i] = IndexOf(textureInAtlasIds, textureInAtlasIdsCount, b.TextureIdForInventory);
                    }
                }
                UseTerrainTextures(textureInAtlasIds, textureInAtlasIdsCount);
                handRedraw = true;
                RedrawAllBlocks();
                break;
            case Packet_ServerIdEnum.ServerRedirect:
                ChatLog("[GAME] Received server redirect");
                //Leave current server
                SendLeave(Packet_LeaveReasonEnum.Leave);
                //Exit game screen and create new game instance
                ExitAndSwitchServer(packet.Redirect);
                break;
        }
    }
    internal ClientPacketHandler[] packetHandlers;

    void CacheAsset(Asset asset)
    {
        //Check if checksum is given (prevents crash on old servers)
        if (asset.md5 == null)
        {
            return;
        }
        //Check if given checksum is valid
        if (!platform.IsChecksum(asset.md5))
        {
            //Skip saving
            return;
        }
        //Only cache a file if it's not already cached
        if (!platform.IsCached(asset.md5))
        {
            platform.SaveAssetToCache(asset);
        }
    }

    void SetFile(string name, string md5, byte[] downloaded, int downloadedLength)
    {
        string nameLowercase = platform.StringToLower(name);
        
        //Create new asset from given data
        Asset newAsset = new Asset();
        newAsset.data = downloaded;
        newAsset.dataLength = downloadedLength;
        newAsset.name = nameLowercase;
        newAsset.md5 = md5;
        
        for (int i = 0; i < assets.count; i++)
        {
            if (assets.items[i] == null)
            {
                continue;
            }
            if (assets.items[i].name == nameLowercase)
            {
                if (options.UseServerTextures)
                {
                    //If server textures are allowed, replace content of current asset
                    assets.items[i] = newAsset;
                }
                //Cache asset for later use
                CacheAsset(newAsset);
                return;
            }
        }
        //Add new asset to asset list
        assets.items[assets.count++] = newAsset;
        
        //Store new asset in cache
        CacheAsset(newAsset);
    }

    internal int handTexture;

    bool ammostarted;
    internal Packet_BlockType[] NewBlockTypes;
    internal string blobdownloadname;
    internal string blobdownloadmd5;
    internal CitoMemoryStream blobdownload;
    internal SunMoonRenderer d_SunMoonRenderer;
    internal int[] NightLevels;
    public const int HourDetail = 4;
    public static int[] ByteArrayToUshortArray(byte[] input, int inputLength)
    {
        int outputLength = inputLength / 2;
        int[] output = new int[outputLength];
        for (int i = 0; i < outputLength; i++)
        {
            output[i] = (input[i * 2 + 1] << 8) + input[i * 2];
        }
        return output;
    }

    internal byte[] GetFile(string p)
    {
        string pLowercase = platform.StringToLower(p);
        for (int i = 0; i < assets.count; i++)
        {
            if (assets.items[i].name == pLowercase)
            {
                return assets.items[i].data;
            }
        }
        return null;
    }

    internal int GetFileLength(string p)
    {
        string pLowercase = platform.StringToLower(p);
        for (int i = 0; i < assets.count; i++)
        {
            if (assets.items[i].name == pLowercase)
            {
                return assets.items[i].dataLength;
            }
        }
        return 0;
    }

    internal void InvalidVersionAllow()
    {
        if (invalidVersionDrawMessage != null)
        {
            invalidVersionDrawMessage = null;
            ProcessServerIdentification(invalidVersionPacketIdentification);
            invalidVersionPacketIdentification = null;
        }
    }

    internal int maxTextureSize; // detected at runtime
    internal int atlas1dheight() { return maxTextureSize; }
    internal int atlas2dtiles() { return GlobalVar.MAX_BLOCKTYPES_SQRT; } // 16x16
    internal TextureAtlasConverter d_TextureAtlasConverter;

    internal void UseTerrainTextureAtlas2d(BitmapCi atlas2d, int atlas2dWidth)
    {
        terrainTexture = platform.LoadTextureFromBitmap(atlas2d);
        int[] terrainTextures1d_;
        int terrainTextures1dCount = 0;
        {
            terrainTexturesPerAtlas = atlas1dheight() / (atlas2dWidth / atlas2dtiles());
            IntRef atlasesidCount = new IntRef();
            BitmapCi[] atlases1d = d_TextureAtlasConverter.Atlas2dInto1d(platform, atlas2d, atlas2dtiles(), atlas1dheight(), atlasesidCount);
            terrainTextures1d_ = new int[atlasesidCount.value];
            for (int i = 0; i < atlasesidCount.value; i++)
            {
                BitmapCi bmp = atlases1d[i];
                int texture = platform.LoadTextureFromBitmap(bmp);
                terrainTextures1d_[terrainTextures1dCount++] = texture;
                platform.BitmapDelete(bmp);
            }
        }
        this.terrainTextures1d = terrainTextures1d_;
    }

    internal void UseTerrainTextures(string[] textureIds, int textureIdsCount)
    {
        //todo bigger than 32x32
        int tilesize = 32;
        BitmapData_ atlas2d = BitmapData_.Create(tilesize * atlas2dtiles(), tilesize * atlas2dtiles());

        for (int i = 0; i < textureIdsCount; i++)
        {
            if (textureIds[i] == null)
            {
                continue;
            }
            byte[] fileData = GetFile(StringTools.StringAppend(platform, textureIds[i], ".png"));
            if (fileData == null)
            {
                fileData = GetFile("Unknown.png");
            }
            if (fileData == null)
            {
                continue;
            }
            BitmapCi bmp = platform.BitmapCreateFromPng(fileData, platform.ByteArrayLength(fileData));
            if (platform.BitmapGetWidth(bmp) != tilesize)
            {
                platform.BitmapDelete(bmp);
                continue;
            }
            if (platform.BitmapGetHeight(bmp) != tilesize)
            {
                platform.BitmapDelete(bmp);
                continue;
            }
            int[] bmpPixels = new int[tilesize * tilesize];
            platform.BitmapGetPixelsArgb(bmp, bmpPixels);

            int x = i % texturesPacked();
            int y = i / texturesPacked();
            for (int xx = 0; xx < tilesize; xx++)
            {
                for (int yy = 0; yy < tilesize; yy++)
                {
                    int c = bmpPixels[xx + yy * tilesize];
                    atlas2d.SetPixel(x * tilesize + xx, y * tilesize + yy, c);
                }
            }

            platform.BitmapDelete(bmp);
        }
        BitmapCi bitmap = platform.BitmapCreate(atlas2d.width, atlas2d.height);
        platform.BitmapSetPixelsArgb(bitmap, atlas2d.argb);
        UseTerrainTextureAtlas2d(bitmap, atlas2d.width);
    }

    int IndexOf(string[] arr, int arrLength, string value)
    {
        for (int i = 0; i < arrLength; i++)
        {
            if (StringEquals(arr[i], value))
            {
                return i;
            }
        }
        return -1;
    }

    public static bool StringEquals(string strA, string strB)
    {
        if (strA == null && strB == null)
        {
            return true;
        }
        if (strA == null || strB == null)
        {
            return false;
        }
        return strA == strB;
    }

    bool Contains(string[] arr, int arrLength, string value)
    {
        return IndexOf(arr, arrLength, value) != -1;
    }

    internal AnimationState localplayeranim;
    internal AnimationHint localplayeranimationhint;

    internal bool enable_move;

    public const int DISCONNECTED_ICON_AFTER_SECONDS = 10;
    internal void KeyDown(int eKey)
    {
        for (int i = 0; i < clientmodsCount; i++)
        {
            KeyEventArgs args_ = new KeyEventArgs();
            args_.SetKeyCode(eKey);
            clientmods[i].OnKeyDown(this, args_);
            if (args_.GetHandled())
            {
                return;
            }
        }
        keyboardState[eKey] = true;
        InvalidVersionAllow();
        if (eKey == GetKey(GlKeys.F6))
        {
            float lagSeconds = one * (platform.TimeMillisecondsFromStart() - LastReceivedMilliseconds) / 1000;
            if ((lagSeconds >= DISCONNECTED_ICON_AFTER_SECONDS) || guistate == GuiState.MapLoading)
            {
                Reconnect();
            }
        }
        if (eKey == GetKey(GlKeys.ShiftLeft) || eKey == GetKey(GlKeys.ShiftRight))
        {
            IsShiftPressed = true;
        }
        if (guistate == GuiState.Normal)
        {
            string strFreemoveNotAllowed = "You are not allowed to enable freemove.";

            if (eKey == GetKey(GlKeys.F1))
            {
                if (!this.AllowFreemove)
                {
                    Log(strFreemoveNotAllowed);
                    return;
                }
                movespeed = basemovespeed * 1;
                Log("Move speed: 1x.");
            }
            if (eKey == GetKey(GlKeys.F2))
            {
                if (!this.AllowFreemove)
                {
                    Log(strFreemoveNotAllowed);
                    return;
                }
                movespeed = basemovespeed * 10;
                Log(platform.StringFormat(language.MoveSpeed(), platform.IntToString(10)));
            }
            if (eKey == GetKey(GlKeys.F3))
            {
                if (!this.AllowFreemove)
                {
                    Log(strFreemoveNotAllowed);
                    return;
                }
                player.physicsState.movedz = 0;
                if (!ENABLE_FREEMOVE)
                {
                    ENABLE_FREEMOVE = true;
                    Log(language.MoveFree());
                }
                else if (ENABLE_FREEMOVE && (!ENABLE_NOCLIP))
                {
                    ENABLE_NOCLIP = true;
                    Log(language.MoveFreeNoclip());
                }
                else if (ENABLE_FREEMOVE && ENABLE_NOCLIP)
                {
                    ENABLE_FREEMOVE = false;
                    ENABLE_NOCLIP = false;
                    Log(language.MoveNormal());
                }
            }
            if (eKey == GetKey(GlKeys.I))
            {
                drawblockinfo = !drawblockinfo;
            }
            int playerx = platform.FloatToInt(player.position.x);
            int playery = platform.FloatToInt(player.position.z);
            if ((playerx >= 0 && playerx < MapSizeX)
                && (playery >= 0 && playery < MapSizeY))
            {
                performanceinfo.Set("height", platform.StringFormat("height:{0}", platform.IntToString(d_Heightmap.GetBlock(playerx, playery))));
            }
            if (eKey == GetKey(GlKeys.F5))
            {
                CameraChange();
            }
            if (eKey == GetKey(GlKeys.Plus) || eKey == GetKey(GlKeys.KeypadPlus))
            {
                if (cameratype == CameraType.Overhead)
                {
                    overheadcameradistance -= 1;
                }
                else if (cameratype == CameraType.Tpp)
                {
                    tppcameradistance -= 1;
                }
            }
            if (eKey == GetKey(GlKeys.Minus) || eKey == GetKey(GlKeys.KeypadMinus))
            {
                if (cameratype == CameraType.Overhead)
                {
                    overheadcameradistance += 1;
                }
                else if (cameratype == CameraType.Tpp)
                {
                    tppcameradistance += 1;
                }
            }
            if (overheadcameradistance < TPP_CAMERA_DISTANCE_MIN) { overheadcameradistance = TPP_CAMERA_DISTANCE_MIN; }
            if (overheadcameradistance > TPP_CAMERA_DISTANCE_MAX) { overheadcameradistance = TPP_CAMERA_DISTANCE_MAX; }

            if (tppcameradistance < TPP_CAMERA_DISTANCE_MIN) { tppcameradistance = TPP_CAMERA_DISTANCE_MIN; }
            if (tppcameradistance > TPP_CAMERA_DISTANCE_MAX) { tppcameradistance = TPP_CAMERA_DISTANCE_MAX; }

            if (eKey == GetKey(GlKeys.F6))
            {
                RedrawAllBlocks();
            }
            if (eKey == GlKeys.F8)
            {
                ToggleVsync();
                if (ENABLE_LAG == 0) { Log(language.FrameRateVsync()); }
                if (ENABLE_LAG == 1) { Log(language.FrameRateUnlimited()); }
                if (ENABLE_LAG == 2) { Log(language.FrameRateLagSimulation()); }
            }
            if (eKey == GetKey(GlKeys.Tab))
            {
                SendPacketClient(ClientPackets.SpecialKeyTabPlayerList());
            }
            if (eKey == GetKey(GlKeys.E))
            {
                if (currentAttackedBlock != null)
                {
                    int posX = currentAttackedBlock.X;
                    int posY = currentAttackedBlock.Y;
                    int posZ = currentAttackedBlock.Z;
                    int blocktype = GetBlock(currentAttackedBlock.X, currentAttackedBlock.Y, currentAttackedBlock.Z);
                    if (IsUsableBlock(blocktype))
                    {
                        if (d_Data.IsRailTile(blocktype))
                        {
                            player.position.x = posX + (one / 2);
                            player.position.y = posZ + 1;
                            player.position.z = posY + (one / 2);
                            ENABLE_FREEMOVE = false;
                        }
                        else
                        {
                            SendSetBlock(posX, posY, posZ, Packet_BlockSetModeEnum.Use, 0, ActiveMaterial);
                        }
                    }
                }
                if (currentlyAttackedEntity != -1)
                {
                    if (entities[currentlyAttackedEntity].usable)
                    {
                        for (int i = 0; i < clientmodsCount; i++)
                        {
                            if (clientmods[i] == null) { continue; }
                            OnUseEntityArgs args = new OnUseEntityArgs();
                            args.entityId = currentlyAttackedEntity;
                            clientmods[i].OnUseEntity(this, args);
                        }
                        SendPacketClient(ClientPackets.UseEntity(currentlyAttackedEntity));
                    }
                }
            }
            if (eKey == GetKey(GlKeys.O))
            {
                Respawn();
            }
            if (eKey == GetKey(GlKeys.L))
            {
                SendPacketClient(ClientPackets.SpecialKeySelectTeam());
            }
            if (eKey == GetKey(GlKeys.P))
            {
                SendPacketClient(ClientPackets.SpecialKeySetSpawn());

                playerPositionSpawnX = player.position.x;
                playerPositionSpawnY = player.position.y;
                playerPositionSpawnZ = player.position.z;

                player.position.x = platform.FloatToInt(player.position.x) + one / 2;
                //player.playerposition.Y = player.playerposition.Y;
                player.position.z = platform.FloatToInt(player.position.z) + one / 2;
            }
            if (eKey == GetKey(GlKeys.F))
            {
                ToggleFog();
                Log(platform.StringFormat(language.FogDistance(), platform.IntToString(platform.FloatToInt(d_Config3d.viewdistance))));
                OnResize();
            }
            if (eKey == GetKey(GlKeys.B))
            {
                ShowInventory();
                return;
            }
            HandleMaterialKeys(eKey);
        }
        if (guistate == GuiState.Inventory)
        {
            if (eKey == GetKey(GlKeys.B)
                || eKey == GetKey(GlKeys.Escape))
            {
                GuiStateBackToGame();
            }
            return;
        }
        if (guistate == GuiState.MapLoading)
        {
            //Return to main menu when ESC key is pressed while loading
            if (eKey == GetKey(GlKeys.Escape))
            {
                ExitToMainMenu_();
            }
        }
        if (guistate == GuiState.CraftingRecipes)
        {
            if (eKey == GetKey(GlKeys.Escape))
            {
                GuiStateBackToGame();
            }
        }
        if (guistate == GuiState.Normal)
        {
            if (eKey == GetKey(GlKeys.Escape))
            {
                EscapeMenuStart();
                return;
            }
        }
    }

    internal bool escapeMenuRestart;
    public void EscapeMenuStart()
    {
        guistate = GuiState.EscapeMenu;
        menustate = new MenuState();
        platform.ExitMousePointerLock();
        escapeMenuRestart = true;
    }

    public void ShowEscapeMenu()
    {
        guistate = GuiState.EscapeMenu;
        menustate = new MenuState();
        SetFreeMouse(true);
    }

    public void ShowInventory()
    {
        guistate = GuiState.Inventory;
        menustate = new MenuState();
        SetFreeMouse(true);
    }

    public void CameraChange()
    {
        if (Follow != null)
        {
            //Prevents switching camera mode when following
            return;
        }
        if (cameratype == CameraType.Fpp)
        {
            cameratype = CameraType.Tpp;
            ENABLE_TPP_VIEW = true;
        }
        else if (cameratype == CameraType.Tpp)
        {
            cameratype = CameraType.Overhead;
            overheadcamera = true;
            SetFreeMouse(true);
            ENABLE_TPP_VIEW = true;
            playerdestination = Vector3Ref.Create(player.position.x, player.position.y, player.position.z);
        }
        else if (cameratype == CameraType.Overhead)
        {
            cameratype = CameraType.Fpp;
            SetFreeMouse(false);
            ENABLE_TPP_VIEW = false;
            overheadcamera = false;
        }
        else
        {
            platform.ThrowException("");
        }
    }
    internal bool drawblockinfo;

    internal void UpdateTitleFps(float dt)
    {
        float elapsed = one * (platform.TimeMillisecondsFromStart() - lasttitleupdateMilliseconds) / 1000;
        if (elapsed >= 1)
        {
            lasttitleupdateMilliseconds = platform.TimeMillisecondsFromStart();
            int chunkupdates = terrainRenderer.ChunkUpdates();
            performanceinfo.Set("chunk updates", platform.StringFormat(language.ChunkUpdates(), platform.IntToString(chunkupdates - lastchunkupdates)));
            lastchunkupdates = terrainRenderer.ChunkUpdates();
            performanceinfo.Set("triangles", platform.StringFormat(language.Triangles(), platform.IntToString(terrainRenderer.TrianglesCount())));
        }
        if (!titleset)
        {
            platform.SetTitle(language.GameName());
            titleset = true;
        }
    }
    bool titleset;

    internal void Draw2d(float dt)
    {
        OrthoMode(Width(), Height());
        switch (guistate)
        {
            case GuiState.Normal:
                {
                    if (!ENABLE_DRAW2D)
                    {
                        PerspectiveMode();
                        return;
                    }
                }
                break;
            case GuiState.Inventory:
                {
                    //d_The3d.ResizeGraphics(Width, Height);
                    //d_The3d.OrthoMode(d_HudInventory.ConstWidth, d_HudInventory.ConstHeight);
                    //d_The3d.PerspectiveMode();
                }
                break;
            case GuiState.MapLoading:
                {
                }
                break;
            case GuiState.ModalDialog:
                {
                }
                break;
            case GuiState.EscapeMenu:
                {
                    if (!ENABLE_DRAW2D)
                    {
                        PerspectiveMode();
                        return;
                    }
                }
                break;
            case GuiState.CraftingRecipes:
                {
                }
                break;
        }
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnNewFrameDraw2d(this, dt);
        }

        //d_The3d.OrthoMode(Width, Height);

        PerspectiveMode();
    }

    public const int ChatFontSize = 11;

    internal BoolRef soundnow;

    internal float movedx;
    internal float movedy;
    internal float pushX;
    internal float pushY;
    internal float pushZ;
    internal bool wantsjump;
    internal bool shiftkeydown;
    internal bool moveup;
    internal bool movedown;
    internal float jumpstartacceleration;
    internal Acceleration acceleration;

    internal void FrameTick(float dt)
    {
        //if ((DateTime.Now - lasttodo).TotalSeconds > BuildDelay && todo.Count > 0)
        //UpdateTerrain();
        for (int i = 0; i < clientmodsCount; i++)
        {
            NewFrameEventArgs args_ = new NewFrameEventArgs();
            args_.SetDt(dt);
            clientmods[i].OnNewFrameFixed(this, args_);
        }
        OnNewFrame(dt);

        if (guistate == GuiState.MapLoading) { return; }

        if (guistate == GuiState.EscapeMenu)
        {
        }
        else if (guistate == GuiState.Inventory)
        {
        }
        else if (guistate == GuiState.MapLoading)
        {
            //todo back to game when escape key pressed.
        }
        else if (guistate == GuiState.CraftingRecipes)
        {
        }
        else if (guistate == GuiState.ModalDialog)
        {
        }

        float orientationX = platform.MathSin(player.position.roty);
        float orientationY = 0;
        float orientationZ = -platform.MathCos(player.position.roty);
        platform.AudioUpdateListener(EyesPosX(), EyesPosY(), EyesPosZ(), orientationX, orientationY, orientationZ);

        playervelocity.X = player.position.x - lastplayerpositionX;
        playervelocity.Y = player.position.y - lastplayerpositionY;
        playervelocity.Z = player.position.z - lastplayerpositionZ;
        playervelocity.X *= 75;
        playervelocity.Y *= 75;
        playervelocity.Z *= 75;
        lastplayerpositionX = player.position.x;
        lastplayerpositionY = player.position.y;
        lastplayerpositionZ = player.position.z;
    }

    public void Update(float dt)
    {
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnNewFrameReadOnlyMainThread(this, dt);
        }
    }

    float lastplayerpositionX;
    float lastplayerpositionY;
    float lastplayerpositionZ;

    public BlockPosSide[] Pick(BlockOctreeSearcher s_, Line3D line, IntRef retCount)
    {
        //pick terrain
        int minX = platform.FloatToInt(MinFloat(line.Start[0], line.End[0]));
        int minY = platform.FloatToInt(MinFloat(line.Start[1], line.End[1]));
        int minZ = platform.FloatToInt(MinFloat(line.Start[2], line.End[2]));
        if (minX < 0) { minX = 0; }
        if (minY < 0) { minY = 0; }
        if (minZ < 0) { minZ = 0; }
        int maxX = platform.FloatToInt(MaxFloat(line.Start[0], line.End[0]));
        int maxY = platform.FloatToInt(MaxFloat(line.Start[1], line.End[1]));
        int maxZ = platform.FloatToInt(MaxFloat(line.Start[2], line.End[2]));
        if (maxX > MapSizeX) { maxX = MapSizeX; }
        if (maxY > MapSizeZ) { maxY = MapSizeZ; }
        if (maxZ > MapSizeY) { maxZ = MapSizeY; }
        int sizex = maxX - minX + 1;
        int sizey = maxY - minY + 1;
        int sizez = maxZ - minZ + 1;
        int size = BitTools.NextPowerOfTwo(MaxInt(sizex, MaxInt(sizey, sizez)));
        s_.StartBox = Box3D.Create(minX, minY, minZ, size);
        //s_.StartBox = Box3D.Create(0, 0, 0, BitTools.NextPowerOfTwo(MaxInt(MapSizeX, MaxInt(MapSizeY, MapSizeZ))));
        BlockPosSide[] pick2 = s_.LineIntersection(IsBlockEmpty_.Create(this), GetBlockHeight_.Create(this), line, retCount);
        PickSort(pick2, retCount.value, line.Start[0], line.Start[1], line.Start[2]);
        return pick2;
    }

    float[] modelViewInverted;

    void PickSort(BlockPosSide[] pick, int pickCount, float x, float y, float z)
    {
        bool changed = false;
        do
        {
            changed = false;
            for (int i = 0; i < pickCount - 1; i++)
            {
                float dist = Dist(pick[i].blockPos[0], pick[i].blockPos[1], pick[i].blockPos[2], x, y, z);
                float distNext = Dist(pick[i + 1].blockPos[0], pick[i + 1].blockPos[1], pick[i + 1].blockPos[2], x, y, z);
                if (dist > distNext)
                {
                    changed = true;

                    BlockPosSide swapTemp = pick[i];
                    pick[i] = pick[i + 1];
                    pick[i + 1] = swapTemp;
                }
            }
        }
        while (changed);
    }

    internal bool mouseLeft;
    internal bool mouseMiddle;
    internal bool mouseRight;

    internal void MouseDown(MouseEventArgs args)
    {
        if (args.GetButton() == MouseButtonEnum.Left) { mouseLeft = true; }
        if (args.GetButton() == MouseButtonEnum.Middle) { mouseMiddle = true; }
        if (args.GetButton() == MouseButtonEnum.Right) { mouseRight = true; }
        if (args.GetButton() == MouseButtonEnum.Left)
        {
            mouseleftclick = true;
        }
        if (args.GetButton() == MouseButtonEnum.Right)
        {
            mouserightclick = true;
        }
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnMouseDown(this, args);
        }
        InvalidVersionAllow();
    }

    internal void MouseUp(MouseEventArgs args)
    {
        if (args.GetButton() == MouseButtonEnum.Left) { mouseLeft = false; }
        if (args.GetButton() == MouseButtonEnum.Middle) { mouseMiddle = false; }
        if (args.GetButton() == MouseButtonEnum.Right) { mouseRight = false; }
        if (args.GetButton() == MouseButtonEnum.Left)
        {
            mouseleftdeclick = true;
        }
        if (args.GetButton() == MouseButtonEnum.Right)
        {
            mouserightdeclick = true;
        }
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnMouseUp(this, args);
        }
    }

    public GamePlatform GetPlatform()
    {
        return platform;
    }

    public void SetPlatform(GamePlatform value)
    {
        platform = value;
    }

    internal int Font;
    internal GameExit d_Exit;

    internal void OnFocusChanged()
    {
        if (guistate == GuiState.Normal)
        {
            EscapeMenuStart();
        }
    }

    internal void OnLoad()
    {
        int maxTextureSize_ = platform.GlGetMaxTextureSize();
        if (maxTextureSize_ < 1024)
        {
            maxTextureSize_ = 1024;
        }
        maxTextureSize = maxTextureSize_;
        //Start();
        //Connect();
        MapLoadingStart();
        platform.GlClearColorRgbaf(0, 0, 0, 1);
        if (d_Config3d.ENABLE_BACKFACECULLING)
        {
            platform.GlDepthMask(true);
            platform.GlEnableDepthTest();
            platform.GlCullFaceBack();
            platform.GlEnableCullFace();
        }
        platform.GlEnableLighting();
        platform.GlEnableColorMaterial();
        platform.GlColorMaterialFrontAndBackAmbientAndDiffuse();
        platform.GlShadeModelSmooth();
    }

    internal void Connect__()
    {
        if (connectdata.ServerPassword == null || connectdata.ServerPassword == "")
        {
            Connect(connectdata.Ip, connectdata.Port, connectdata.Username, connectdata.Auth);
        }
        else
        {
            Connect_(connectdata.Ip, connectdata.Port, connectdata.Username, connectdata.Auth, connectdata.ServerPassword);
        }
        MapLoadingStart();
    }

    public void OnRenderFrame(float deltaTime)
    {
        TaskScheduler(deltaTime);
    }

    internal float[] camera;
    float accumulator;
    internal void MainThreadOnRenderFrame(float deltaTime)
    {
        UpdateResize();

        if (guistate == GuiState.MapLoading)
        {
            platform.GlClearColorRgbaf(0, 0, 0, 1);
        }
        else
        {
            platform.GlClearColorRgbaf(one * Game.clearcolorR / 255, one * Game.clearcolorG / 255, one * Game.clearcolorB / 255, one * Game.clearcolorA / 255);
        }

        if (guistate == GuiState.Normal && enableCameraControl)
        {
            UpdateMouseViewportControl(deltaTime);
        }

        //Sleep is required in Mono for running the terrain background thread.
        platform.ApplicationDoEvents();

        accumulator += deltaTime;
        float dt = one / 75;

        while (accumulator >= dt)
        {
            FrameTick(dt);
            accumulator -= dt;
        }

        if (guistate == GuiState.MapLoading)
        {
            GotoDraw2d(deltaTime);
            return;
        }

        if (ENABLE_LAG == 2)
        {
            platform.ThreadSpinWait(20 * 1000 * 1000);
        }

        SetAmbientLight(terraincolor());
        UpdateTitleFps(deltaTime);
        platform.GlClearColorBufferAndDepthBuffer();
        platform.BindTexture2d(d_TerrainTextures.terrainTexture());

        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnBeforeNewFrameDraw3d(this, deltaTime);
        }
        GLMatrixModeModelView();
        GLLoadMatrix(camera);
        CameraMatrix.lastmvmatrix = camera;

        d_FrustumCulling.CalcFrustumEquations();

        bool drawgame = guistate != GuiState.MapLoading;
        if (drawgame)
        {
            platform.GlEnableDepthTest();
            for (int i = 0; i < clientmodsCount; i++)
            {
                if (clientmods[i] == null) { continue; }
                clientmods[i].OnNewFrameDraw3d(this, deltaTime);
            }
        }
        GotoDraw2d(deltaTime);
    }

    int lastWidth;
    int lastHeight;
    void UpdateResize()
    {
        if (lastWidth != platform.GetCanvasWidth()
            || lastHeight != platform.GetCanvasHeight())
        {
            lastWidth = platform.GetCanvasWidth();
            lastHeight = platform.GetCanvasHeight();
            OnResize();
        }
    }

    bool startedconnecting;
    internal void GotoDraw2d(float dt)
    {
        SetAmbientLight(Game.ColorFromArgb(255, 255, 255, 255));
        Draw2d(dt);

        for (int i = 0; i < clientmodsCount; i++)
        {
            NewFrameEventArgs args_ = new NewFrameEventArgs();
            args_.SetDt(dt);
            clientmods[i].OnNewFrame(this, args_);
        }

        mouseleftclick = mouserightclick = false;
        mouseleftdeclick = mouserightdeclick = false;
        if ((!issingleplayer)
            || (issingleplayer && platform.SinglePlayerServerLoaded())
            || (!platform.SinglePlayerServerAvailable()))
        {
            if (!startedconnecting)
            {
                startedconnecting = true;
                Connect__();
            }
        }
    }

    public float Scale()
    {
        //Only scale things on mobile devices
        if (platform.IsSmallScreen())
        {
            float scale = one * Width() / 1280;
            return scale;
        }
        else
        {
            return one;
        }
    }

    public void OnTouchStart(TouchEventArgs e)
    {
        InvalidVersionAllow();
        mouseCurrentX = e.GetX();
        mouseCurrentY = e.GetY();
        mouseleftclick = true;

        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnTouchStart(this, e);
            if (e.GetHandled())
            {
                return;
            }
        }
    }

    internal float touchMoveDx;
    internal float touchMoveDy;
    internal float touchOrientationDx;
    internal float touchOrientationDy;

    public void OnTouchMove(TouchEventArgs e)
    {
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnTouchMove(this, e);
            if (e.GetHandled())
            {
                return;
            }
        }
    }

    public void OnTouchEnd(TouchEventArgs e)
    {
        mouseCurrentX = 0;
        mouseCurrentY = 0;
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnTouchEnd(this, e);
            if (e.GetHandled())
            {
                return;
            }
        }
    }

    public void OnBackPressed()
    {
    }

    public void MouseMove(MouseEventArgs e)
    {
        mouseCurrentX = e.GetX();
        mouseCurrentY = e.GetY();
        mouseDeltaX = e.GetMovementX();
        mouseDeltaY = e.GetMovementY();
        for (int i = 0; i < clientmodsCount; i++)
        {
            if (clientmods[i] == null) { continue; }
            clientmods[i].OnMouseMove(this, e);
        }
    }

    TaskScheduler_ scheduler;

    void TaskScheduler(float deltaTime)
    {
        scheduler.Update(deltaTime);
    }

    public void QueueTaskReadOnlyBackgroundPerFrame(Task task)
    {
        scheduler.QueueTaskReadOnlyBackgroundPerFrame(task);
    }

    public void QueueTaskCommit(Task task)
    {
        scheduler.QueueTaskCommit(task);
    }

    public void QueueTaskReadOnlyMainThread(Task task)
    {
        scheduler.QueueTaskReadOnlyMainThread(task);
    }

    public void DrawModel(Model model)
    {
        SetMatrixUniformModelView();
        platform.DrawModel(model);
    }

    public void DrawModels(Model[] model, int count)
    {
        SetMatrixUniformModelView();
        platform.DrawModels(model, count);
    }

    public void DrawModelData(ModelData data)
    {
        SetMatrixUniformModelView();
        platform.DrawModelData(data);
    }

    public void Dispose()
    {
        terrainRenderer.Clear();
        for (int i = 0; i < textures.count; i++)
        {
            if (textures.items[i] == null)
            {
                continue;
            }
            platform.GLDeleteTexture(textures.items[i].value);
        }
        for (int i = 0; i < cachedTextTexturesMax; i++)
        {
            if (cachedTextTextures[i] == null)
            {
                continue;
            }
            if (cachedTextTextures[i].texture == null)
            {
                continue;
            }
            platform.GLDeleteTexture(cachedTextTextures[i].texture.textureId);
        }
    }
    
    public void StartTyping()
    {
        GuiTyping = TypingState.Typing;
        IsTyping = true;
        GuiTypingBuffer = "";
        IsTeamchat = false;
    }

    public void StopTyping()
    {
        GuiTyping = TypingState.None;
    }

    internal float sunPositionX;
    internal float sunPositionY;
    internal float sunPositionZ;
    internal float moonPositionX;
    internal float moonPositionY;
    internal float moonPositionZ;
    internal bool isNight;
    internal bool fancySkysphere;

    internal static float Angle256ToRad(int value)
    {
        float one_ = 1;
        return ((one_ * value) / 255) * GetPi() * 2;
    }

    internal static float RadToAngle256(float value)
    {
        return (value / (2 * GetPi())) * 255;
    }
}

public class DictionaryStringAudioSample
{
    public DictionaryStringAudioSample()
    {
        max = 1024;
        count = 0;
        keys = new string[max];
        values = new AudioSampleCi[max];
    }

    string[] keys;
    AudioSampleCi[] values;
    int max;
    int count;

    public void Set(string key, AudioSampleCi value)
    {
        int index = GetIndex(key);
        if (index != -1)
        {
            values[index] = value;
            return;
        }
        keys[count] = key;
        values[count] = value;
        count++;
    }

    public bool Contains(string key)
    {
        int index = GetIndex(key);
        return index != -1;
    }

    public AudioSampleCi Get(string key)
    {
        int index = GetIndex(key);
        return values[index];
    }

    public int GetIndex(string key)
    {
        for (int i = 0; i < count; i++)
        {
            if (keys[i] == key)
            {
                return i;
            }
        }
        return -1;
    }
}

public class GameScreen : ClientMod
{
    public GameScreen()
    {
        WidgetCount = 64;
        widgets = new MenuWidget[WidgetCount];
    }
    internal Game game;
    public override void OnKeyPress(Game game_, KeyPressEventArgs args) { KeyPress(args); }
    public override void OnTouchStart(Game game_, TouchEventArgs e) { ScreenOnTouchStart(e); }
    public void ScreenOnTouchStart(TouchEventArgs e)
    {
        e.SetHandled(MouseDown(e.GetX(), e.GetY()));
    }
    public override void OnTouchEnd(Game game_, TouchEventArgs e) { ScreenOnTouchEnd(e); }
    public void ScreenOnTouchEnd(TouchEventArgs e)
    {
        MouseUp(e.GetX(), e.GetY());
    }
    public override void OnMouseDown(Game game_, MouseEventArgs args) { MouseDown(args.GetX(), args.GetY()); }
    public override void OnMouseUp(Game game_, MouseEventArgs args) { MouseUp(args.GetX(), args.GetY()); }
    public override void OnMouseMove(Game game_, MouseEventArgs args) { MouseMove(args); }
    public virtual void OnBackPressed() { }
    
    void KeyPress(KeyPressEventArgs e)
    {
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                if (w.type == WidgetType.Textbox)
                {
                    if (w.editing)
                    {
                        string s = CharToString(e.GetKeyChar());
                        if (e.GetKeyChar() == 8) // backspace
                        {
                            if (StringTools.StringLength(game.platform, w.text) > 0)
                            {
                                w.text = StringTools.StringSubstring(game.platform, w.text, 0, StringTools.StringLength(game.platform, w.text) - 1);
                            }
                            return;
                        }
                        if (e.GetKeyChar() == 9 || e.GetKeyChar() == 13) // tab, enter
                        {
                            return;
                        }
                        if (e.GetKeyChar() == 22) //paste
                        {
                            if (game.platform.ClipboardContainsText())
                            {
                                w.text = StringTools.StringAppend(game.platform, w.text, game.platform.ClipboardGetText());
                            }
                            return;
                        }
                        if (game.platform.IsValidTypingChar(e.GetKeyChar()))
                        {
                            w.text = StringTools.StringAppend(game.platform, w.text, s);
                        }
                    }
                }
            }
        }
    }

    bool MouseDown(int x, int y)
    {
        bool handled = false;
        bool editingChange = false;
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                if (w.type == WidgetType.Button)
                {
                    w.pressed = pointInRect(x, y, screenx + w.x, screeny + w.y, w.sizex, w.sizey);
                    if (w.pressed) { handled = true; }
                }
                if (w.type == WidgetType.Textbox)
                {
                    w.pressed = pointInRect(x, y, screenx + w.x, screeny + w.y, w.sizex, w.sizey);
                    if (w.pressed) { handled = true; }
                    bool wasEditing = w.editing;
                    w.editing = w.pressed;
                    if (w.editing && (!wasEditing))
                    {
                        game.platform.ShowKeyboard(true);
                        editingChange = true;
                    }
                    if ((!w.editing) && wasEditing && (!editingChange))
                    {
                        game.platform.ShowKeyboard(false);
                    }
                }
            }
        }
        return handled;
    }

    void MouseUp(int x, int y)
    {
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                w.pressed = false;
            }
        }
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                if (w.type == WidgetType.Button)
                {
                    if (pointInRect(x, y, screenx + w.x, screeny + w.y, w.sizex, w.sizey))
                    {
                        OnButton(w);
                    }
                }
            }
        }
    }

    public virtual void OnButton(MenuWidget w) { }

    void MouseMove(MouseEventArgs e)
    {
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                w.hover = pointInRect(e.GetX(), e.GetY(), screenx + w.x, screeny + w.y, w.sizex, w.sizey);
            }
        }
    }

    bool pointInRect(float x, float y, float rx, float ry, float rw, float rh)
    {
        return x >= rx && y >= ry && x < rx + rw && y < ry + rh;
    }

    public virtual void OnMouseWheel(MouseWheelEventArgs e) { }
    internal int WidgetCount;
    internal MenuWidget[] widgets;
    public void DrawWidgets()
    {
        for (int i = 0; i < WidgetCount; i++)
        {
            MenuWidget w = widgets[i];
            if (w != null)
            {
                if (!w.visible)
                {
                    continue;
                }
                string text = w.text;
                if (w.selected)
                {
                    text = StringTools.StringAppend(game.platform, "&2", text);
                }
                if (w.type == WidgetType.Button)
                {
                    if (w.buttonStyle == ButtonStyle.Text)
                    {
                        //game.Draw2dText1(text, w.fontSize, w.x, w.y + w.sizey / 2, TextAlign.Left, TextBaseline.Middle);
                    }
                    else
                    {
                        if (w.image != null)
                        {
                            game.Draw2dBitmapFile(w.image, screenx + w.x, screeny + w.y, w.sizex, w.sizey);
                        }
                        else
                        {
                            game.Draw2dTexture(game.WhiteTexture(), screenx + w.x, screeny + w.y, w.sizex, w.sizey, null, 0, w.color, false);
                        }
                        game.Draw2dText1(text, screenx + game.platform.FloatToInt(w.x), screeny + game.platform.FloatToInt(w.y + w.sizey / 2), game.platform.FloatToInt(w.fontSize), null, false);
                    }
                }
                if (w.type == WidgetType.Textbox)
                {
                    if (w.password)
                    {
                        text = CharRepeat(42, StringTools.StringLength(game.platform, w.text)); // '*'
                    }
                    if (w.editing)
                    {
                        text = StringTools.StringAppend(game.platform, text, "_");
                    }
                    //if (w.buttonStyle == ButtonStyle.Text)
                    {
                        game.Draw2dText(text, w.font, screenx + w.x, screeny + w.y, null, false);//, TextAlign.Left, TextBaseline.Top);
                    }
                    //else
                    {
                        //menu.DrawButton(text, w.fontSize, w.x, w.y, w.sizex, w.sizey, (w.hover || w.editing));
                    }
                }
                if (w.type == WidgetType.Label)
                {
                    game.Draw2dText(text, w.font, screenx + w.x, screeny + w.y, null, false);
                }
                if (w.description != null)
                {
                    //menu.DrawText(w.description, w.fontSize, w.x, w.y + w.sizey / 2, TextAlign.Right, TextBaseline.Middle);
                }
            }
        }
    }
    public string CharToString(int a)
    {
        int[] arr = new int[1];
        arr[0] = a;
        return game.platform.CharArrayToString(arr, 1);
    }

    public string CharRepeat(int c, int length)
    {
        int[] charArray = new int[length];
        for (int i = 0; i < length; i++)
        {
            charArray[i] = c;
        }
        return game.platform.CharArrayToString(charArray, length);
    }
    internal int screenx;
    internal int screeny;
}

public class UpdateTask : Task
{
    public override void Run(float dt)
    {
        game.Update(dt);
        game.QueueTaskReadOnlyMainThread(this); // todo
    }
}

public class NetworkProcessTask : Task
{
    public NetworkProcessTask()
    {
        CurrentChunk = new byte[1024 * 64];
        CurrentChunkCount = 0;
        receivedchunk = new int[32 * 32 * 32];
        decompressedchunk = new byte[32 * 32 * 32 * 2];
    }
    internal byte[] CurrentChunk;
    internal int CurrentChunkCount;
    int[] receivedchunk;
    byte[] decompressedchunk;

#if CITO
    macro Index3d(x, y, h, sizex, sizey) ((((((h) * (sizey)) + (y))) * (sizex)) + (x))
#else
    static int Index3d(int x, int y, int h, int sizex, int sizey)
    {
        return (h * sizey + y) * sizex + x;
    }
#endif

    public override void Run(float dt)
    {
        NetworkProcess();
        game.QueueTaskReadOnlyBackgroundPerFrame(this);
    }

    public void NetworkProcess()
    {
        game.currentTimeMilliseconds = game.platform.TimeMillisecondsFromStart();
        if (game.main == null)
        {
            return;
        }
        NetIncomingMessage msg;
        for (; ; )
        {
            if (game.invalidVersionPacketIdentification != null)
            {
                break;
            }
            msg = game.main.ReadMessage();
            if (msg == null)
            {
                break;
            }
            TryReadPacket(msg.message, msg.messageLength);
        }
    }

    public void TryReadPacket(byte[] data, int dataLength)
    {
        Packet_Server packet = new Packet_Server();
        Packet_ServerSerializer.DeserializeBuffer(data, dataLength, packet);

        ProcessInBackground(packet);

        ProcessPacketTask task = new ProcessPacketTask();
        task.game = game;
        task.packet = packet;
        game.QueueTaskCommit(task);

        game.LastReceivedMilliseconds = game.currentTimeMilliseconds;
        //return lengthPrefixLength + packetLength;
    }

    void ProcessInBackground(Packet_Server packet)
    {
        switch (packet.Id)
        {
            case Packet_ServerIdEnum.ChunkPart:
                byte[] arr = packet.ChunkPart.CompressedChunkPart;
                int arrLength = game.platform.ByteArrayLength(arr); // todo
                for (int i = 0; i < arrLength; i++)
                {
                    CurrentChunk[CurrentChunkCount++] = arr[i];
                }
                break;
            case Packet_ServerIdEnum.Chunk_:
                {
                    Packet_ServerChunk p = packet.Chunk_;
                    if (CurrentChunkCount != 0)
                    {
                        game.platform.GzipDecompress(CurrentChunk, CurrentChunkCount, decompressedchunk);
                        {
                            int i = 0;
                            for (int zz = 0; zz < p.SizeZ; zz++)
                            {
                                for (int yy = 0; yy < p.SizeY; yy++)
                                {
                                    for (int xx = 0; xx < p.SizeX; xx++)
                                    {
                                        receivedchunk[Index3d(xx, yy, zz, p.SizeX, p.SizeY)] = (decompressedchunk[i + 1] << 8) + decompressedchunk[i];
                                        i += 2;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int size = p.SizeX * p.SizeY * p.SizeZ;
                        for (int i = 0; i < size; i++)
                        {
                            receivedchunk[i] = 0;
                        }
                    }
                    {
                        game.SetMapPortion(p.X, p.Y, p.Z, receivedchunk, p.SizeX, p.SizeY, p.SizeZ);
                        for (int xx = 0; xx < 2; xx++)
                        {
                            for (int yy = 0; yy < 2; yy++)
                            {
                                for (int zz = 0; zz < 2; zz++)
                                {
                                    //d_Shadows.OnSetChunk(p.X + 16 * xx, p.Y + 16 * yy, p.Z + 16 * zz);//todo
                                }
                            }
                        }
                    }
                    game.ReceivedMapLength += CurrentChunkCount;// lengthPrefixLength + packetLength;
                    CurrentChunkCount = 0;
                }
                break;
            case Packet_ServerIdEnum.HeightmapChunk:
                {
                    Packet_ServerHeightmapChunk p = packet.HeightmapChunk;
                    game.platform.GzipDecompress(p.CompressedHeightmap, game.platform.ByteArrayLength(p.CompressedHeightmap), decompressedchunk);
                    int[] decompressedchunk1 = Game.ByteArrayToUshortArray(decompressedchunk, p.SizeX * p.SizeY * 2);
                    for (int xx = 0; xx < p.SizeX; xx++)
                    {
                        for (int yy = 0; yy < p.SizeY; yy++)
                        {
                            int height = decompressedchunk1[MapUtilCi.Index2d(xx, yy, p.SizeX)];
                            game.d_Heightmap.SetBlock(p.X + xx, p.Y + yy, height);
                        }
                    }
                }
                break;
        }
    }
}

public class ProcessPacketTask : Task
{
    internal Packet_Server packet;

    public override void Run(float dt)
    {
        game.ProcessPacket(packet);
    }
}


public abstract class Action_
{
    public abstract void Run();
}

public class TaskAction : Action_
{
    public static TaskAction Create(Task task)
    {
        TaskAction action = new TaskAction();
        action.task = task;
        return action;
    }
    internal Task task;
    public override void Run()
    {
        task.Run(1);
        task.Done = true;
    }
}

public class Task
{
    internal Game game;
    public virtual void Run(float dt) { }
    internal bool Done;
}

public class TaskScheduler_
{
    public TaskScheduler_()
    {
        mainTasks = QueueTask.Create(128);
        backgroundPerFrameTasks = ListTask.Create(128);
        commitTasks = QueueTask.Create(16 * 1024);
        tasks = QueueTask.Create(16 * 1024);
        newPerFrameTasks = QueueTask.Create(128);
    }

    public void Start(GamePlatform platform_)
    {
        platform = platform_;
        lockObject = platform.MonitorCreate();
    }

    GamePlatform platform;
    MonitorObject lockObject;

    public void QueueTaskReadOnlyMainThread(Task task)
    {
        platform.MonitorEnter(lockObject);
        mainTasks.Enqueue(task);
        platform.MonitorExit(lockObject);
    }

    QueueTask newPerFrameTasks;

    public void QueueTaskReadOnlyBackgroundPerFrame(Task task)
    {
        platform.MonitorEnter(lockObject);
        newPerFrameTasks.Enqueue(task);
        platform.MonitorExit(lockObject);
    }

    public void QueueTaskCommit(Task task)
    {
        platform.MonitorEnter(lockObject);
        commitTasks.Enqueue(task);
        platform.MonitorExit(lockObject);
    }

    QueueTask mainTasks;
    ListTask backgroundPerFrameTasks;
    QueueTask commitTasks;

    QueueTask tasks;

    public void Update(float dt)
    {
        Move(mainTasks, tasks);
        while (tasks.Count() > 0)
        {
            tasks.Dequeue().Run(dt);
        }

        if (platform.MultithreadingAvailable())
        {
            for (int i = backgroundPerFrameTasks.count - 1; i >= 0; i--)
            {
                if (backgroundPerFrameTasks.items[i].Done)
                {
                    backgroundPerFrameTasks.RemoveAt(i);
                }
            }
            if (backgroundPerFrameTasks.Count() == 0)
            {
                Move(commitTasks, tasks);
                while (tasks.Count() > 0)
                {
                    tasks.Dequeue().Run(dt);
                }

                Move(newPerFrameTasks, tasks);
                while (tasks.Count() > 0)
                {
                    Task task = tasks.Dequeue();
                    backgroundPerFrameTasks.Add(task);
                    task.Done = false;
                    platform.QueueUserWorkItem(TaskAction.Create(task));
                }
            }
        }
        else
        {
            for (int i = 0; i < backgroundPerFrameTasks.count; i++)
            {
                backgroundPerFrameTasks.items[i].Run(dt);
            }
            backgroundPerFrameTasks.Clear();

            Move(commitTasks, tasks);
            while (tasks.Count() > 0)
            {
                tasks.Dequeue().Run(dt);
            }

            Move(newPerFrameTasks, tasks);
            while (tasks.Count() > 0)
            {
                Task task = tasks.Dequeue();
                backgroundPerFrameTasks.Add(task);
                task.Done = false;
            }
        }
    }

    void Move(QueueTask from, QueueTask to)
    {
        platform.MonitorEnter(lockObject);
        int count = from.count_;
        for (int i = 0; i < count; i++)
        {
            Task task = from.Dequeue();
            to.Enqueue(task);
        }
        platform.MonitorExit(lockObject);
    }
}

public class QueueTask
{
    public QueueTask()
    {
        Start(128);
    }
    public static QueueTask Create(int max_)
    {
        QueueTask queue = new QueueTask();
        queue.Start(max_);
        return queue;
    }

    void Start(int max)
    {
        max_ = max;
        items = new Task[max_];
        count_ = 0;
    }

    internal Task[] items;
    internal int start_;
    internal int count_;
    internal int max_;

    public void Enqueue(Task value)
    {
        if (count_ == max_)
        {
            Resize(max_ * 2);
        }
        int pos = start_ + count_;
        pos = pos % max_;
        count_++;
        items[pos] = value;
    }

    void Resize(int newSize)
    {
        Task[] items2 = new Task[newSize];
        for (int i = 0; i < max_; i++)
        {
            items2[i] = items[(start_ + i) % max_];
        }
        items = items2;
        start_ = 0;
        max_ = newSize;
    }

    public Task Dequeue()
    {
        Task ret = items[start_];
        items[start_] = null;
        start_++;
        start_ = start_ % max_;
        count_--;
        return ret;
    }

    public int Count()
    {
        return count_;
    }
}

public class QueueAction
{
    public QueueAction()
    {
        Start(128);
    }
    public static QueueAction Create(int max_)
    {
        QueueAction queue = new QueueAction();
        queue.Start(max_);
        return queue;
    }

    void Start(int max_)
    {
        max = max_;
        items = new Action_[max_];
        count = 0;
    }

    internal Action_[] items;
    internal int start;
    internal int count;
    internal int max;

    public void Enqueue(Action_ value)
    {
        if (count == max)
        {
            Resize(max * 2);
        }
        int pos = start + count;
        pos = pos % max;
        count++;
        items[pos] = value;
    }

    void Resize(int newSize)
    {
        Action_[] items2 = new Action_[newSize];
        for (int i = 0; i < max; i++)
        {
            items2[i] = items[(start + i) % max];
        }
        items = items2;
        start = 0;
        max = newSize;
    }

    public Action_ Dequeue()
    {
        Action_ ret = items[start];
        items[start] = null;
        start++;
        start = start % max;
        count--;
        return ret;
    }

    public int Count()
    {
        return count;
    }
}

public class ListTask
{
    public static ListTask Create(int max_)
    {
        ListTask l = new ListTask();
        l.Start(max_);
        return l;
    }

    public void Start(int max_)
    {
        max = max_;
        items = new Task[max_];
        count = 0;
    }

    internal int max;
    internal Task[] items;
    internal int count;

    internal void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            items[i] = null;
        }
        count = 0;
    }

    internal void RemoveAt(int index)
    {
        for (int i = index; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }
        count--;
    }

    internal int Count()
    {
        return count;
    }

    internal void Add(Task task)
    {
        items[count++] = task;
    }
}

public class DrawTask : Task
{
    public override void Run(float dt)
    {
        game.MainThreadOnRenderFrame(dt);
        game.QueueTaskReadOnlyMainThread(this);
    }
}

public class LoginData
{
    internal string ServerAddress;
    internal int Port;
    internal string AuthCode; //Md5(private server key + player name)
    internal string Token;

    internal bool PasswordCorrect;
    internal bool ServerCorrect;
}

public class LoginClientCi
{
    internal LoginResultRef loginResult;
    public void Login(GamePlatform platform, string user, string password, string publicServerKey, string token, LoginResultRef result, LoginData resultLoginData_)
    {
        loginResult = result;
        resultLoginData = resultLoginData_;
        result.value = LoginResult.Connecting;

        LoginUser = user;
        LoginPassword = password;
        LoginToken = token;
        LoginPublicServerKey = publicServerKey;
        shouldLogin = true;
    }
    string LoginUser;
    string LoginPassword;
    string LoginToken;
    string LoginPublicServerKey;

    bool shouldLogin;
    string loginUrl;
    HttpResponseCi loginUrlResponse;
    HttpResponseCi loginResponse;
    LoginData resultLoginData;
    public void Update(GamePlatform platform)
    {
        if (loginResult == null)
        {
            return;
        }

        if (loginUrlResponse == null && loginUrl == null)
        {
            loginUrlResponse = new HttpResponseCi();
            platform.WebClientDownloadDataAsync("http://manicdigger.sourceforge.net/login.php", loginUrlResponse);
        }
        if (loginUrlResponse != null && loginUrlResponse.done)
        {
            loginUrl = platform.StringFromUtf8ByteArray(loginUrlResponse.value, loginUrlResponse.valueLength);
            loginUrlResponse = null;
        }

        if (loginUrl != null)
        {
            if (shouldLogin)
            {
                shouldLogin = false;
                string requestString = platform.StringFormat4("username={0}&password={1}&server={2}&token={3}"
                    , LoginUser, LoginPassword, LoginPublicServerKey, LoginToken);
                IntRef byteArrayLength = new IntRef();
                byte[] byteArray = platform.StringToUtf8ByteArray(requestString, byteArrayLength);
                loginResponse = new HttpResponseCi();
                platform.WebClientUploadDataAsync(loginUrl, byteArray, byteArrayLength.value, loginResponse);
            }
            if (loginResponse != null && loginResponse.done)
            {
                string responseString = platform.StringFromUtf8ByteArray(loginResponse.value, loginResponse.valueLength);
                resultLoginData.PasswordCorrect = !(platform.StringContains(responseString, "Wrong username") || platform.StringContains(responseString, "Incorrect username"));
                resultLoginData.ServerCorrect = !platform.StringContains(responseString, "server");
                if (resultLoginData.PasswordCorrect)
                {
                    loginResult.value = LoginResult.Ok;
                }
                else
                {
                    loginResult.value = LoginResult.Failed;
                }
                IntRef linesCount = new IntRef();
                string[] lines = platform.ReadAllLines(responseString, linesCount);
                if (linesCount.value >= 3)
                {
                    resultLoginData.AuthCode = lines[0];
                    resultLoginData.ServerAddress = lines[1];
                    resultLoginData.Port = platform.IntParse(lines[2]);
                    resultLoginData.Token = lines[3];
                }
                loginResponse = null;
            }
        }
    }
}

public class GameExit
{
    internal bool exit;
    internal bool restart;

    public void SetExit(bool p)
    {
        exit = p;
    }

    public bool GetExit()
    {
        return exit;
    }
    
    public void SetRestart(bool p)
    {
        restart = p;
    }

    public bool GetRestart()
    {
        return restart;
    }
}

public class TileEnterData
{
    internal int BlockPositionX;
    internal int BlockPositionY;
    internal int BlockPositionZ;
    internal TileEnterDirection EnterDirection;
}

public class UpDown
{
    public const int None = 0;
    public const int Up = 1;
    public const int Down = 2;
}

class StringByteArray
{
    internal string name;
    internal byte[] data;
}

class DictionaryStringByteArray
{
    public DictionaryStringByteArray()
    {
        items = new StringByteArray[1024];
        itemsCount = 1024;
    }
    internal StringByteArray[] items;
    internal int itemsCount;

    internal void Set(string name, byte[] value)
    {
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i] == null) { continue; }
            if (Game.StringEquals(items[i].name, name))
            {
                items[i].data = value;
                return;
            }
        }
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i] == null)
            {
                items[i] = new StringByteArray();
                items[i].name = name;
                items[i].data = value;
                return;
            }
        }
    }

    internal byte[] Get(string name)
    {
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i] == null) { continue; }
            if (Game.StringEquals(items[i].name, name))
            {
                return items[i].data;
            }
        }
        return null;
    }
}

public class RenderHintEnum
{
    public const int Fast = 0;
    public const int Nice = 1;
}

public class Speculative
{
    internal int x;
    internal int y;
    internal int z;
    internal int timeMilliseconds;
    internal int blocktype;
}

public class TimerCi
{
    public TimerCi()
    {
        interval = 1;
        maxDeltaTime = -1;
    }
    internal float interval;
    internal float maxDeltaTime;

    internal float accumulator;
    public void Reset()
    {
        accumulator = 0;
    }
    public int Update(float dt)
    {
        accumulator += dt;
        float constDt = interval;
        if (maxDeltaTime != -1 && accumulator > maxDeltaTime)
        {
            accumulator = maxDeltaTime;
        }
        int updates = 0;
        while (accumulator >= constDt)
        {
            updates++;
            accumulator -= constDt;
        }
        return updates;
    }

    internal static TimerCi Create(int interval_, int maxDeltaTime_)
    {
        TimerCi timer = new TimerCi();
        timer.interval = interval_;
        timer.maxDeltaTime = maxDeltaTime_;
        return timer;
    }
}

public class GetBlockHeight_ : DelegateGetBlockHeight
{
    public static GetBlockHeight_ Create(Game w_)
    {
        GetBlockHeight_ g = new GetBlockHeight_();
        g.w = w_;
        return g;
    }
    internal Game w;
    public override float GetBlockHeight(int x, int y, int z)
    {
        return w.getblockheight(x, y, z);
    }
}

public class IsBlockEmpty_ : DelegateIsBlockEmpty
{
    public static IsBlockEmpty_ Create(Game w_)
    {
        IsBlockEmpty_ g = new IsBlockEmpty_();
        g.w = w_;
        return g;
    }
    Game w;
    public override bool IsBlockEmpty(int x, int y, int z)
    {
        return w.IsTileEmptyForPhysics(x, y, z);
    }
}

public class Sprite
{
    public Sprite()
    {
        size = 40;
    }
    internal float positionX;
    internal float positionY;
    internal float positionZ;
    internal string image;
    internal int size;
    internal int animationcount;
}

public class PlayerDrawInfo
{
    public PlayerDrawInfo()
    {
        anim = new AnimationState();
        AnimationHint_ = new AnimationHint();
    }
    internal AnimationState anim;
    internal NetworkInterpolation interpolation;
    internal float lastnetworkposX;
    internal float lastnetworkposY;
    internal float lastnetworkposZ;
    internal float lastcurposX;
    internal float lastcurposY;
    internal float lastcurposZ;
    internal float lastnetworkrotx;
    internal float lastnetworkroty;
    internal float lastnetworkrotz;
    internal float velocityX;
    internal float velocityY;
    internal float velocityZ;
    internal bool moves;
    internal AnimationHint AnimationHint_;
}

public class PlayerInterpolate : IInterpolation
{
    internal GamePlatform platform;
    public override InterpolatedObject Interpolate(InterpolatedObject a, InterpolatedObject b, float progress)
    {
        PlayerInterpolationState aa = platform.CastToPlayerInterpolationState(a);
        PlayerInterpolationState bb = platform.CastToPlayerInterpolationState(b);
        PlayerInterpolationState cc = new PlayerInterpolationState();
        cc.positionX = aa.positionX + (bb.positionX - aa.positionX) * progress;
        cc.positionY = aa.positionY + (bb.positionY - aa.positionY) * progress;
        cc.positionZ = aa.positionZ + (bb.positionZ - aa.positionZ) * progress;
        //cc.heading = Game.IntToByte(AngleInterpolation.InterpolateAngle256(platform, aa.heading, bb.heading, progress));
        //cc.pitch = Game.IntToByte(AngleInterpolation.InterpolateAngle256(platform, aa.pitch, bb.pitch, progress));
        cc.rotx = DegToRad(AngleInterpolation.InterpolateAngle360(platform, RadToDeg(aa.rotx), RadToDeg(bb.rotx), progress));
        cc.roty = DegToRad(AngleInterpolation.InterpolateAngle360(platform, RadToDeg(aa.roty), RadToDeg(bb.roty), progress));
        cc.rotz = DegToRad(AngleInterpolation.InterpolateAngle360(platform, RadToDeg(aa.rotz), RadToDeg(bb.rotz), progress));
        return cc;
    }
    public static float RadToDeg(float rad)
    {
        return (rad / (2 * Game.GetPi())) * 360;
    }
    public static float DegToRad(float deg)
    {
        return (deg / 360) * 2 * Game.GetPi();
    }
}

public class PlayerInterpolationState : InterpolatedObject
{
    internal float positionX;
    internal float positionY;
    internal float positionZ;
    internal float rotx;
    internal float roty;
    internal float rotz;
    internal byte heading;
    internal byte pitch;
}

public class Bullet_
{
    internal float fromX;
    internal float fromY;
    internal float fromZ;
    internal float toX;
    internal float toY;
    internal float toZ;
    internal float speed;
    internal float progress;
}

public class Expires
{
    internal static Expires Create(float p)
    {
        Expires expires = new Expires();
        expires.totalTime = p;
        expires.timeLeft = p;
        return expires;
    }

    internal float totalTime;
    internal float timeLeft;
}

public class DrawName
{
    internal float TextX;
    internal float TextY;
    internal float TextZ;
    internal string Name;
    internal bool DrawHealth;
    internal float Health;
    internal bool OnlyWhenSelected;
    internal bool ClientAutoComplete;
}

public class Entity
{
    internal Expires expires;
    internal Sprite sprite;
    internal Grenade_ grenade;
    internal Bullet_ bullet;
    internal Minecart minecart;
    internal PlayerDrawInfo playerDrawInfo;
    internal CharacterPhysicsState physicsState;

    // network
    internal EntityPosition_ networkPosition;
    internal EntityPosition_ position;
    internal DrawName drawName;
    internal EntityDrawModel drawModel;
    internal EntityDrawText drawText;
    internal Packet_ServerExplosion push;
    internal bool usable;
    internal Packet_ServerPlayerStats playerStats;
    internal EntityDrawArea drawArea;
}

public class EntityDrawArea
{
    internal int x;
    internal int y;
    internal int z;
    internal int sizex;
    internal int sizey;
    internal int sizez;
    internal bool visible;
}

public class EntityPosition_
{
    internal float x;
    internal float y;
    internal float z;
    internal float rotx;
    internal float roty;
    internal float rotz;

    internal bool PositionLoaded;
    internal int LastUpdateMilliseconds;
}

public class EntityDrawModel
{
    public EntityDrawModel()
    {
        CurrentTexture = -1;
    }
    internal float eyeHeight;
    internal string Model_;
    internal float ModelHeight;
    internal string Texture_;
    internal bool DownloadSkin;

    internal int CurrentTexture;
    internal HttpResponseCi SkinDownloadResponse;
    internal AnimatedModelRenderer renderer;
}

public class EntityDrawText
{
    internal float dx;
    internal float dy;
    internal float dz;
    internal float rotx;
    internal float roty;
    internal float rotz;
    internal string text;
}

public class DictionaryVector3Float
{
    public DictionaryVector3Float()
    {
        itemsCount = 16 * 1024;
        items = new Vector3Float[itemsCount];
    }
    internal Vector3Float[] items;
    internal int itemsCount;
    internal bool ContainsKey(int x, int y, int z)
    {
        return ItemIndex(x, y, z) != -1;
    }

    int ItemIndex(int x, int y, int z)
    {
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i] == null)
            {
                continue;
            }
            Vector3Float item = items[i];
            if (item.x == x && item.y == y && item.z == z)
            {
                return i;
            }
        }
        return -1;
    }

    internal float Get(int x, int y, int z)
    {
        return items[ItemIndex(x, y, z)].value;
    }

    internal void Remove(int x, int y, int z)
    {
        if (ItemIndex(x, y, z) == -1)
        {
            return;
        }
        items[ItemIndex(x, y, z)] = null;
    }

    internal void Set(int x, int y, int z, float value)
    {
        int index = ItemIndex(x, y, z);
        if (index != -1)
        {
            items[index].value = value;
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                if (items[i] == null)
                {
                    Vector3Float item = new Vector3Float();
                    item.x = x;
                    item.y = y;
                    item.z = z;
                    item.value = value;
                    items[i] = item;
                    return;
                }
            }
        }
    }

    internal int Count()
    {
        int count = 0;
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    internal void Clear()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            items[i] = null;
        }
    }
}

public class Vector3Float
{
    internal int x;
    internal int y;
    internal int z;
    internal float value;
}

public class VisibleDialog
{
    internal string key;
    internal Packet_Dialog value;
    internal GameScreen screen;
}

public class RailMapUtil
{
    internal Game game;
    public RailSlope GetRailSlope(int x, int y, int z)
    {
        int tiletype = game.GetBlock(x, y, z);
        int railDirectionFlags = game.blocktypes[tiletype].Rail;
        int blocknear;
        if (x < game.MapSizeX - 1)
        {
            blocknear = game.GetBlock(x + 1, y, z);
            if (railDirectionFlags == RailDirectionFlags.Horizontal &&
                 blocknear != 0 && game.blocktypes[blocknear].Rail == 0)
            {
                return RailSlope.TwoRightRaised;
            }
        }
        if (x > 0)
        {
            blocknear = game.GetBlock(x - 1, y, z);
            if (railDirectionFlags == RailDirectionFlags.Horizontal &&
                 blocknear != 0 && game.blocktypes[blocknear].Rail == 0)
            {
                return RailSlope.TwoLeftRaised;

            }
        }
        if (y > 0)
        {
            blocknear = game.GetBlock(x, y - 1, z);
            if (railDirectionFlags == RailDirectionFlags.Vertical &&
                  blocknear != 0 && game.blocktypes[blocknear].Rail == 0)
            {
                return RailSlope.TwoUpRaised;
            }
        }
        if (y < game.MapSizeY - 1)
        {
            blocknear = game.GetBlock(x, y + 1, z);
            if (railDirectionFlags == RailDirectionFlags.Vertical &&
                  blocknear != 0 && game.blocktypes[blocknear].Rail == 0)
            {
                return RailSlope.TwoDownRaised;
            }
        }
        return RailSlope.Flat;
    }
}

public class RailDirectionFlags
{
    public const int None = 0;
    public const int Horizontal = 1;
    public const int Vertical = 2;
    public const int UpLeft = 4;
    public const int UpRight = 8;
    public const int DownLeft = 16;
    public const int DownRight = 32;

    public const int Full = Horizontal | Vertical | UpLeft | UpRight | DownLeft | DownRight;
    public const int TwoHorizontalVertical = Horizontal | Vertical;
    public const int Corners = UpLeft | UpRight | DownLeft | DownRight;
}

public enum RailSlope
{
    Flat, TwoLeftRaised, TwoRightRaised, TwoUpRaised, TwoDownRaised
}

public enum RailDirection
{
    Horizontal,
    Vertical,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public enum TileExitDirection
{
    Up,
    Down,
    Left,
    Right
}

public enum TileEnterDirection
{
    Up,
    Down,
    Left,
    Right
}

/// <summary>
/// Each RailDirection on tile can be traversed by train in two directions.
/// </summary>
/// <example>
/// RailDirection.Horizontal -> VehicleDirection12.HorizontalLeft (vehicle goes left and decreases x position),
/// and VehicleDirection12.HorizontalRight (vehicle goes right and increases x position).
/// </example>
public enum VehicleDirection12
{
    HorizontalLeft,
    HorizontalRight,
    VerticalUp,
    VerticalDown,

    UpLeftUp,
    UpLeftLeft,
    UpRightUp,
    UpRightRight,

    DownLeftDown,
    DownLeftLeft,
    DownRightDown,
    DownRightRight
}

public class VehicleDirection12Flags
{
    public const int None = 0;
    public const int HorizontalLeft = 1 << 0;
    public const int HorizontalRight = 1 << 1;
    public const int VerticalUp = 1 << 2;
    public const int VerticalDown = 1 << 3;

    public const int UpLeftUp = 1 << 4;
    public const int UpLeftLeft = 1 << 5;
    public const int UpRightUp = 1 << 6;
    public const int UpRightRight = 1 << 7;

    public const int DownLeftDown = 1 << 8;
    public const int DownLeftLeft = 1 << 9;
    public const int DownRightDown = 1 << 10;
    public const int DownRightRight = 1 << 11;
}

public class DirectionUtils
{
    /// <summary>
    /// VehicleDirection12.UpRightRight -> returns Direction4.Right
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static TileExitDirection ResultExit(VehicleDirection12 direction)
    {
        switch (direction)
        {
            case VehicleDirection12.HorizontalLeft:
                return TileExitDirection.Left;
            case VehicleDirection12.HorizontalRight:
                return TileExitDirection.Right;
            case VehicleDirection12.VerticalUp:
                return TileExitDirection.Up;
            case VehicleDirection12.VerticalDown:
                return TileExitDirection.Down;

            case VehicleDirection12.UpLeftUp:
                return TileExitDirection.Up;
            case VehicleDirection12.UpLeftLeft:
                return TileExitDirection.Left;
            case VehicleDirection12.UpRightUp:
                return TileExitDirection.Up;
            case VehicleDirection12.UpRightRight:
                return TileExitDirection.Right;

            case VehicleDirection12.DownLeftDown:
                return TileExitDirection.Down;
            case VehicleDirection12.DownLeftLeft:
                return TileExitDirection.Left;
            case VehicleDirection12.DownRightDown:
                return TileExitDirection.Down;
            case VehicleDirection12.DownRightRight:
                return TileExitDirection.Right;
            default:
                return TileExitDirection.Down;
        }
    }

    public static RailDirection ToRailDirection(VehicleDirection12 direction)
    {
        switch (direction)
        {
            case VehicleDirection12.HorizontalLeft:
                return RailDirection.Horizontal;
            case VehicleDirection12.HorizontalRight:
                return RailDirection.Horizontal;
            case VehicleDirection12.VerticalUp:
                return RailDirection.Vertical;
            case VehicleDirection12.VerticalDown:
                return RailDirection.Vertical;

            case VehicleDirection12.UpLeftUp:
                return RailDirection.UpLeft;
            case VehicleDirection12.UpLeftLeft:
                return RailDirection.UpLeft;
            case VehicleDirection12.UpRightUp:
                return RailDirection.UpRight;
            case VehicleDirection12.UpRightRight:
                return RailDirection.UpRight;

            case VehicleDirection12.DownLeftDown:
                return RailDirection.DownLeft;
            case VehicleDirection12.DownLeftLeft:
                return RailDirection.DownLeft;
            case VehicleDirection12.DownRightDown:
                return RailDirection.DownRight;
            case VehicleDirection12.DownRightRight:
                return RailDirection.DownRight;
            default:
                return RailDirection.DownLeft;
        }
    }

    public static int ToRailDirectionFlags(RailDirection direction)
    {
        switch (direction)
        {
            case RailDirection.DownLeft:
                return RailDirectionFlags.DownLeft;
            case RailDirection.DownRight:
                return RailDirectionFlags.DownRight;
            case RailDirection.Horizontal:
                return RailDirectionFlags.Horizontal;
            case RailDirection.UpLeft:
                return RailDirectionFlags.UpLeft;
            case RailDirection.UpRight:
                return RailDirectionFlags.UpRight;
            case RailDirection.Vertical:
                return RailDirectionFlags.Vertical;
            default:
                return 0;
        }
    }

    public static VehicleDirection12 Reverse(VehicleDirection12 direction)
    {
        switch (direction)
        {
            case VehicleDirection12.HorizontalLeft:
                return VehicleDirection12.HorizontalRight;
            case VehicleDirection12.HorizontalRight:
                return VehicleDirection12.HorizontalLeft;
            case VehicleDirection12.VerticalUp:
                return VehicleDirection12.VerticalDown;
            case VehicleDirection12.VerticalDown:
                return VehicleDirection12.VerticalUp;

            case VehicleDirection12.UpLeftUp:
                return VehicleDirection12.UpLeftLeft;
            case VehicleDirection12.UpLeftLeft:
                return VehicleDirection12.UpLeftUp;
            case VehicleDirection12.UpRightUp:
                return VehicleDirection12.UpRightRight;
            case VehicleDirection12.UpRightRight:
                return VehicleDirection12.UpRightUp;

            case VehicleDirection12.DownLeftDown:
                return VehicleDirection12.DownLeftLeft;
            case VehicleDirection12.DownLeftLeft:
                return VehicleDirection12.DownLeftDown;
            case VehicleDirection12.DownRightDown:
                return VehicleDirection12.DownRightRight;
            case VehicleDirection12.DownRightRight:
                return VehicleDirection12.DownRightDown;
            default:
                return VehicleDirection12.DownLeftDown;
        }
    }

    public static int ToVehicleDirection12Flags(VehicleDirection12 direction)
    {
        switch (direction)
        {
            case VehicleDirection12.HorizontalLeft:
                return VehicleDirection12Flags.HorizontalLeft;
            case VehicleDirection12.HorizontalRight:
                return VehicleDirection12Flags.HorizontalRight;
            case VehicleDirection12.VerticalUp:
                return VehicleDirection12Flags.VerticalUp;
            case VehicleDirection12.VerticalDown:
                return VehicleDirection12Flags.VerticalDown;

            case VehicleDirection12.UpLeftUp:
                return VehicleDirection12Flags.UpLeftUp;
            case VehicleDirection12.UpLeftLeft:
                return VehicleDirection12Flags.UpLeftLeft;
            case VehicleDirection12.UpRightUp:
                return VehicleDirection12Flags.UpRightUp;
            case VehicleDirection12.UpRightRight:
                return VehicleDirection12Flags.UpRightRight;

            case VehicleDirection12.DownLeftDown:
                return VehicleDirection12Flags.DownLeftDown;
            case VehicleDirection12.DownLeftLeft:
                return VehicleDirection12Flags.DownLeftLeft;
            case VehicleDirection12.DownRightDown:
                return VehicleDirection12Flags.DownRightDown;
            case VehicleDirection12.DownRightRight:
                return VehicleDirection12Flags.DownRightRight;
            default:
                return 0;
        }
    }

    public static TileEnterDirection ResultEnter(TileExitDirection direction)
    {
        switch (direction)
        {
            case TileExitDirection.Up:
                return TileEnterDirection.Down;
            case TileExitDirection.Down:
                return TileEnterDirection.Up;
            case TileExitDirection.Left:
                return TileEnterDirection.Right;
            case TileExitDirection.Right:
                return TileEnterDirection.Left;
            default:
                return TileEnterDirection.Down;
        }
    }
    public static int RailDirectionFlagsCount(int railDirectionFlags)
    {
        int count = 0;
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.DownLeft)) != 0) { count++; }
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.DownRight)) != 0) { count++; }
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.Horizontal)) != 0) { count++; }
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.UpLeft)) != 0) { count++; }
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.UpRight)) != 0) { count++; }
        if ((railDirectionFlags & DirectionUtils.ToRailDirectionFlags(RailDirection.Vertical)) != 0) { count++; }
        return count;
    }

    public static int ToVehicleDirection12Flags_(VehicleDirection12[] directions, int directionsCount)
    {
        int flags = VehicleDirection12Flags.None;
        for (int i = 0; i < directionsCount; i++)
        {
            VehicleDirection12 d = directions[i];
            flags = flags | DirectionUtils.ToVehicleDirection12Flags(d);
        }
        return flags;
    }

    /// <summary>
    /// Enter at TileEnterDirection.Left -> yields VehicleDirection12.UpLeftUp,
    /// VehicleDirection12.HorizontalRight,
    /// VehicleDirection12.DownLeftDown
    /// </summary>
    /// <param name="enter_at"></param>
    /// <returns></returns>
    public static VehicleDirection12[] PossibleNewRails3(TileEnterDirection enter_at)
    {
        VehicleDirection12[] ret = new VehicleDirection12[3];
        switch (enter_at)
        {
            case TileEnterDirection.Left:
                ret[0] = VehicleDirection12.UpLeftUp;
                ret[1] = VehicleDirection12.HorizontalRight;
                ret[2] = VehicleDirection12.DownLeftDown;
                break;
            case TileEnterDirection.Down:
                ret[0] = VehicleDirection12.DownLeftLeft;
                ret[1] = VehicleDirection12.VerticalUp;
                ret[2] = VehicleDirection12.DownRightRight;
                break;
            case TileEnterDirection.Up:
                ret[0] = VehicleDirection12.UpLeftLeft;
                ret[1] = VehicleDirection12.VerticalDown;
                ret[2] = VehicleDirection12.UpRightRight;
                break;
            case TileEnterDirection.Right:
                ret[0] = VehicleDirection12.UpRightUp;
                ret[1] = VehicleDirection12.HorizontalLeft;
                ret[2] = VehicleDirection12.DownRightDown;
                break;
            default:
                return null;
        }
        return ret;
    }
}

public class ClientInventoryController : IInventoryController
{
    public static ClientInventoryController Create(Game game)
    {
        ClientInventoryController c = new ClientInventoryController();
        c.g = game;
        return c;
    }

    Game g;

    public override void InventoryClick(Packet_InventoryPosition pos)
    {
        g.InventoryClick(pos);
    }

    public override void WearItem(Packet_InventoryPosition from, Packet_InventoryPosition to)
    {
        g.WearItem(from, to);
    }

    public override void MoveToInventory(Packet_InventoryPosition from)
    {
        g.MoveToInventory(from);
    }
}

public enum CameraType
{
    Fpp,
    Tpp,
    Overhead
}

public enum TypingState
{
    None,
    Typing,
    Ready
}

public class Player
{
    public Player()
    {
        AnimationHint_ = new AnimationHint();
        Model = "player.txt";
        EyeHeight = DefaultEyeHeight();
        ModelHeight = DefaultModelHeight();
        CurrentTexture = -1;
    }
    internal bool PositionLoaded;
    internal float PositionX;
    internal float PositionY;
    internal float PositionZ;
    internal byte Heading;
    internal byte Pitch;
    internal string Name;
    internal AnimationHint AnimationHint_;
    internal PlayerType Type;
    internal int MonsterType;
    internal int Health;
    internal int LastUpdateMilliseconds;
    internal string Model;
    internal string Texture;
    internal float EyeHeight;
    internal float ModelHeight;
    internal float NetworkX;
    internal float NetworkY;
    internal float NetworkZ;
    internal byte NetworkHeading;
    internal byte NetworkPitch;
    internal PlayerDrawInfo playerDrawInfo;
    internal bool moves;
    internal int CurrentTexture;
    internal HttpResponseCi SkinDownloadResponse;

    public float DefaultEyeHeight()
    {
        float one = 1;
        return one * 15 / 10;
    }

    public float DefaultModelHeight()
    {
        float one = 1;
        return one * 17 / 10;
    }
}

public enum PlayerType
{
    Player,
    Monster
}

public class Grenade_
{
    internal float velocityX;
    internal float velocityY;
    internal float velocityZ;
    internal int block;
    internal int sourcePlayer;
}

public class GetCameraMatrix : IGetCameraMatrix
{
    internal float[] lastmvmatrix;
    internal float[] lastpmatrix;
    public override float[] GetModelViewMatrix()
    {
        return lastmvmatrix;
    }

    public override float[] GetProjectionMatrix()
    {
        return lastpmatrix;
    }
}

public class MenuState
{
    internal int selected;
}

public enum EscapeMenuState
{
    Main,
    Options,
    Graphics,
    Keys,
    Other
}

public class MapLoadingProgressEventArgs
{
    internal int ProgressPercent;
    internal int ProgressBytes;
    internal string ProgressStatus;
}

public class Draw2dData
{
    internal float x1;
    internal float y1;
    internal float width;
    internal float height;
    internal IntRef inAtlasId;
    internal int color;
}

public class Chunk
{
    internal byte[] data;
    internal int[] dataInt;
    internal int LastUpdate;
    internal bool IsPopulated;
    internal int LastChange;
    internal RenderedChunk rendered;
}

public class ChunkEntityClient
{
    
}

public class RenderedChunk
{
    public RenderedChunk()
    {
        dirty = true;
        shadowsdirty = true;
    }
    internal int[] ids;
    internal int idsCount;
    internal bool dirty;
    internal bool shadowsdirty;
    internal byte[] light;
}

public class ITerrainTextures
{
    internal Game game;

    public int texturesPacked() { return game.texturesPacked(); }
    public int terrainTexture() { return game.terrainTexture; }
    public int[] terrainTextures1d() { return game.terrainTextures1d; }
    public int terrainTexturesPerAtlas() { return game.terrainTexturesPerAtlas; }
}

public class Config3d
{
    public Config3d()
    {
        ENABLE_BACKFACECULLING = true;
        ENABLE_TRANSPARENCY = true;
        ENABLE_MIPMAPS = true;
        ENABLE_VISIBILITY_CULLING = false;
        viewdistance = 128;
    }
    internal bool ENABLE_BACKFACECULLING;
    internal bool ENABLE_TRANSPARENCY;
    internal bool ENABLE_MIPMAPS;
    internal bool ENABLE_VISIBILITY_CULLING;
    internal float viewdistance;
    public float GetViewDistance() { return viewdistance; }
    public void SetViewDistance(float value) { viewdistance = value; }
    public bool GetEnableTransparency() { return ENABLE_TRANSPARENCY; }
    public void SetEnableTransparency(bool value) { ENABLE_TRANSPARENCY = value; }
    public bool GetEnableMipmaps() { return ENABLE_MIPMAPS; }
    public void SetEnableMipmaps(bool value) { ENABLE_MIPMAPS = value; }
}

public class MapUtilCi
{
    public static int Index3d(int x, int y, int h, int sizex, int sizey)
    {
        return (h * sizey + y) * sizex + x;
    }

    public static int Index2d(int x, int y, int sizex)
    {
        return x + y * sizex;
    }

    public static void Pos(int index, int sizex, int sizey, Vector3Ref ret)
    {
        int x = index % sizex;
        int y = (index / sizex) % sizey;
        int h = index / (sizex * sizey);
        ret.X = x;
        ret.Y = y;
        ret.Z = h;
    }

    internal static void PosInt(int index, int sizex, int sizey, Vector3IntRef ret)
    {
        int x = index % sizex;
        int y = (index / sizex) % sizey;
        int h = index / (sizex * sizey);
        ret.X = x;
        ret.Y = y;
        ret.Z = h;
    }
}

public class InfiniteMapChunked2d
{
    internal Game d_Map;
    public const int chunksize = 16;
    internal int[][] chunks;
    public int GetBlock(int x, int y)
    {
        int[] chunk = GetChunk(x, y);
        return chunk[MapUtilCi.Index2d(x % chunksize, y % chunksize, chunksize)];
    }
    public int[] GetChunk(int x, int y)
    {
        int[] chunk = null;
        int kx = x / chunksize;
        int ky = y / chunksize;
        if (chunks[MapUtilCi.Index2d(kx, ky, d_Map.MapSizeX / chunksize)] == null)
        {
            chunk = new int[chunksize * chunksize];// (byte*)Marshal.AllocHGlobal(chunksize * chunksize);
            for (int i = 0; i < chunksize * chunksize; i++)
            {
                chunk[i] = 0;
            }
            chunks[MapUtilCi.Index2d(kx, ky, d_Map.MapSizeX / chunksize)] = chunk;
        }
        chunk = chunks[MapUtilCi.Index2d(kx, ky, d_Map.MapSizeX / chunksize)];
        return chunk;
    }
    public void SetBlock(int x, int y, int blocktype)
    {
        GetChunk(x, y)[MapUtilCi.Index2d(x % chunksize, y % chunksize, chunksize)] = blocktype;
    }
    public void Restart()
    {
        //chunks = new byte[d_Map.MapSizeX / chunksize, d_Map.MapSizeY / chunksize][,];
        int n = (d_Map.MapSizeX / chunksize) * (d_Map.MapSizeY / chunksize);
        chunks = new int[n][];//(byte**)Marshal.AllocHGlobal(n * sizeof(IntPtr));
        for (int i = 0; i < n; i++)
        {
            chunks[i] = null;
        }
    }
    public void ClearChunk(int x, int y)
    {
        int px = x / chunksize;
        int py = y / chunksize;
        chunks[MapUtilCi.Index2d(px, py, d_Map.MapSizeX / chunksize)] = null;
    }
}

public abstract class ClientModManager
{
    public abstract void MakeScreenshot();
    public abstract void SetLocalPosition(float glx, float gly, float glz);
    public abstract float GetLocalPositionX();
    public abstract float GetLocalPositionY();
    public abstract float GetLocalPositionZ();
    public abstract void SetLocalOrientation(float glx, float gly, float glz);
    public abstract float GetLocalOrientationX();
    public abstract float GetLocalOrientationY();
    public abstract float GetLocalOrientationZ();
    public abstract void DisplayNotification(string message);
    public abstract void SendChatMessage(string message);
    public abstract GamePlatform GetPlatform();
    public abstract void ShowGui(int level);
    public abstract void SetFreemove(int level);
    public abstract int GetFreemove();
    public abstract BitmapCi GrabScreenshot();
    public abstract AviWriterCi AviWriterCreate();
    public abstract int GetWindowWidth();
    public abstract int GetWindowHeight();
    public abstract bool IsFreemoveAllowed();
    public abstract void EnableCameraControl(bool enable);
    public abstract int WhiteTexture();
    public abstract void Draw2dTexture(int textureid, float x1, float y1, float width, float height, IntRef inAtlasId, int color);
    public abstract void Draw2dTextures(Draw2dData[] todraw, int todrawLength, int textureId);
    public abstract void Draw2dText(string text, float x, float y, float fontsize);
    public abstract void OrthoMode();
    public abstract void PerspectiveMode();
    public abstract DictionaryStringString GetPerformanceInfo();
}

public class ClientModManager1 : ClientModManager
{
    internal Game game;

    public override void MakeScreenshot()
    {
        game.platform.SaveScreenshot();
    }

    public override void SetLocalPosition(float glx, float gly, float glz)
    {
        game.player.position.x = glx;
        game.player.position.y = gly;
        game.player.position.z = glz;
    }

    public override float GetLocalPositionX()
    {
        return game.player.position.x;
    }

    public override float GetLocalPositionY()
    {
        return game.player.position.y;
    }

    public override float GetLocalPositionZ()
    {
        return game.player.position.z;
    }

    public override void SetLocalOrientation(float glx, float gly, float glz)
    {
        game.player.position.rotx = glx;
        game.player.position.roty = gly;
        game.player.position.rotz = glz;
    }

    public override float GetLocalOrientationX()
    {
        return game.player.position.rotx;
    }

    public override float GetLocalOrientationY()
    {
        return game.player.position.roty;
    }

    public override float GetLocalOrientationZ()
    {
        return game.player.position.rotz;
    }

    public override void DisplayNotification(string message)
    {
        game.AddChatline(message);
    }

    public override void SendChatMessage(string message)
    {
        game.SendChat(message);
    }

    public override GamePlatform GetPlatform()
    {
        return game.platform;
    }

    public override void ShowGui(int level)
    {
        if (level == 0)
        {
            game.ENABLE_DRAW2D = false;
        }
        else
        {
            game.ENABLE_DRAW2D = true;
        }
    }

    public override void SetFreemove(int level)
    {
        if (level == FreemoveLevelEnum.None)
        {
            game.ENABLE_FREEMOVE = false;
            game.ENABLE_NOCLIP = false;
        }

        if (level == FreemoveLevelEnum.Freemove)
        {
            game.ENABLE_FREEMOVE = true;
            game.ENABLE_NOCLIP = false;
        }

        if (level == FreemoveLevelEnum.Noclip)
        {
            game.ENABLE_FREEMOVE = true;
            game.ENABLE_NOCLIP = true;
        }
    }

    public override int GetFreemove()
    {
        if (!game.ENABLE_FREEMOVE)
        {
            return FreemoveLevelEnum.None;
        }
        if (game.ENABLE_NOCLIP)
        {
            return FreemoveLevelEnum.Noclip;
        }
        else
        {
            return FreemoveLevelEnum.Freemove;
        }
    }

    public override BitmapCi GrabScreenshot()
    {
        return game.platform.GrabScreenshot();
    }

    public override AviWriterCi AviWriterCreate()
    {
        return game.platform.AviWriterCreate();
    }

    public override int GetWindowWidth()
    {
        return game.platform.GetCanvasWidth();
    }

    public override int GetWindowHeight()
    {
        return game.platform.GetCanvasHeight();
    }

    public override bool IsFreemoveAllowed()
    {
        return game.AllowFreemove;
    }

    public override void EnableCameraControl(bool enable)
    {
        game.enableCameraControl = enable;
    }

    public override int WhiteTexture()
    {
        return game.WhiteTexture();
    }

    public override void Draw2dTexture(int textureid, float x1, float y1, float width, float height, IntRef inAtlasId, int color)
    {
        int a = Game.ColorA(color);
        int r = Game.ColorR(color);
        int g = Game.ColorG(color);
        int b = Game.ColorB(color);
        game.Draw2dTexture(textureid, game.platform.FloatToInt(x1), game.platform.FloatToInt(y1),
            game.platform.FloatToInt(width), game.platform.FloatToInt(height),
            inAtlasId, 0, Game.ColorFromArgb(a, r, g, b), false);
    }

    public override void Draw2dTextures(Draw2dData[] todraw, int todrawLength, int textureId)
    {
        game.Draw2dTextures(todraw, todrawLength, textureId);
    }


    public override void Draw2dText(string text, float x, float y, float fontsize)
    {
        FontCi font = new FontCi();
        font.family = "Arial";
        font.size = fontsize;
        game.Draw2dText(text, font, x, y, null, false);
    }

    public override void OrthoMode()
    {
        game.OrthoMode(GetWindowWidth(), GetWindowHeight());
    }

    public override void PerspectiveMode()
    {
        game.PerspectiveMode();
    }

    public override DictionaryStringString GetPerformanceInfo()
    {
        return game.performanceinfo;
    }
}

public abstract class AviWriterCi
{
    public abstract void Open(string filename, int framerate, int width, int height);
    public abstract void AddFrame(BitmapCi bitmap);
    public abstract void Close();
}

public class BitmapCi
{
    public virtual void Dispose() { }
}

public class FreemoveLevelEnum
{
    public const int None = 0;
    public const int Freemove = 1;
    public const int Noclip = 2;
}

public abstract class ClientMod
{
    public virtual void Start(ClientModManager modmanager) { }
    public virtual bool OnClientCommand(Game game, ClientCommandArgs args) { return false; }
    public virtual void OnNewFrame(Game game, NewFrameEventArgs args) { }
    public virtual void OnNewFrameFixed(Game game, NewFrameEventArgs args) { }
    public virtual void OnNewFrameDraw2d(Game game, float deltaTime) { }
    public virtual void OnBeforeNewFrameDraw3d(Game game, float deltaTime) { }
    public virtual void OnNewFrameDraw3d(Game game, float deltaTime) { }
    public virtual void OnNewFrameReadOnlyMainThread(Game game, float deltaTime) { }
    public virtual void OnKeyDown(Game game, KeyEventArgs args) { }
    public virtual void OnKeyPress(Game game, KeyPressEventArgs args) { }
    public virtual void OnKeyUp(Game game, KeyEventArgs args) { }
    public virtual void OnMouseUp(Game game, MouseEventArgs args) { }
    public virtual void OnMouseDown(Game game, MouseEventArgs args) { }
    public virtual void OnMouseMove(Game game, MouseEventArgs args) { }
    public virtual void OnMouseWheelChanged(Game game, MouseWheelEventArgs args) { }
    public virtual void OnTouchStart(Game game, TouchEventArgs e) { }
    public virtual void OnTouchMove(Game game, TouchEventArgs e) { }
    public virtual void OnTouchEnd(Game game, TouchEventArgs e) { }
    public virtual void OnUseEntity(Game game, OnUseEntityArgs e) { }
    public virtual void OnHitEntity(Game game, OnUseEntityArgs e) { }
}

public class OnUseEntityArgs
{
    internal int entityId;
}

public class ClientCommandArgs
{
    internal string command;
    internal string arguments;
}

public class TextureAtlasCi
{
    public static void TextureCoords2d(int textureId, int texturesPacked, RectFRef r)
    {
        float one = 1;
        r.y = (one / texturesPacked * (textureId / texturesPacked));
        r.x = (one / texturesPacked * (textureId % texturesPacked));
        r.w = one / texturesPacked;
        r.h = one / texturesPacked;
    }
}

public class StackMatrix4
{
    public StackMatrix4()
    {
        values = new float[max][];
        for (int i = 0; i < max; i++)
        {
            values[i] = Mat4.Create();
        }
    }
    float[][] values;
    const int max = 1024;
    int count_;

    internal void Push(float[] p)
    {
        Mat4.Copy(values[count_], p);
        count_++;
    }

    internal float[] Peek()
    {
        return values[count_ - 1];
    }

    internal int Count()
    {
        return count_;
    }

    internal float[] Pop()
    {
        float[] ret = values[count_ - 1];
        count_--;
        return ret;
    }
}

public class CachedTexture
{
    internal int textureId;
    internal float sizeX;
    internal float sizeY;
    internal int lastuseMilliseconds;
}

public class Text_
{
    internal string text;
    internal float fontsize;
    internal int color;
    internal string fontfamily;
    internal int fontstyle;

    internal bool Equals_(Text_ t)
    {
        return this.text == t.text
            && this.fontsize == t.fontsize
            && this.color == t.color
            && this.fontfamily == t.fontfamily
            && this.fontstyle == t.fontstyle;
    }

    public string GetText() { return text; } public void SetText(string value) { text = value; }
    public float GetFontSize() { return fontsize; } public void SetFontSize(float value) { fontsize = value; }
    public int GetColor() { return color; } public void SetColor(int value) { color = value; }
    public string GetFontFamily() { return fontfamily; } public void SetFontFamily(string value) { fontfamily = value; }
    public int GetFontStyle() { return fontstyle; } public void SetFontStyle(int value) { fontstyle = value; }
}

public class CachedTextTexture
{
    internal Text_ text;
    internal CachedTexture texture;
}

public class FontCi
{
    internal string family;
    internal float size;
    internal int style;

    internal static FontCi Create(string family_, float size_, int style_)
    {
        FontCi f = new FontCi();
        f.family = family_;
        f.size = size_;
        f.style = style_;
        return f;
    }
}

public class TextPart
{
    internal int color;
    internal string text;
}

public class TextColorRenderer
{
    internal GamePlatform platform;

    internal BitmapCi CreateTextTexture(Text_ t)
    {
        IntRef partsCount = new IntRef();
        TextPart[] parts = DecodeColors(t.text, t.color, partsCount);

        float totalwidth = 0;
        float totalheight = 0;
        int[] sizesX = new int[partsCount.value];
        int[] sizesY = new int[partsCount.value];

        for (int i = 0; i < partsCount.value; i++)
        {
            IntRef outWidth = new IntRef();
            IntRef outHeight = new IntRef();
            platform.TextSize(parts[i].text, t.fontsize, outWidth, outHeight);

            sizesX[i] = outWidth.value;
            sizesY[i] = outHeight.value;

            totalwidth += outWidth.value;
            totalheight = Game.MaxFloat(totalheight, outHeight.value);
        }

        int size2X = NextPowerOfTwo(platform.FloatToInt(totalwidth) + 1);
        int size2Y = NextPowerOfTwo(platform.FloatToInt(totalheight) + 1);
        BitmapCi bmp2 = platform.BitmapCreate(size2X, size2Y);
        int[] bmp2Pixels = new int[size2X * size2Y];

        float currentwidth = 0;
        for (int i = 0; i < partsCount.value; i++)
        {
            int sizeiX = sizesX[i];
            int sizeiY = sizesY[i];
            if (sizeiX == 0 || sizeiY == 0)
            {
                continue;
            }
            Text_ partText = new Text_();
            partText.text = parts[i].text;
            partText.color = parts[i].color;
            partText.fontsize = t.fontsize;
            partText.fontstyle = t.fontstyle;
            partText.fontfamily = t.fontfamily;
            BitmapCi partBmp = platform.CreateTextTexture(partText);
            int partWidth = platform.FloatToInt(platform.BitmapGetWidth(partBmp));
            int partHeight = platform.FloatToInt(platform.BitmapGetHeight(partBmp));
            int[] partBmpPixels = new int[partWidth * partHeight];
            platform.BitmapGetPixelsArgb(partBmp, partBmpPixels);
            for (int x = 0; x < partWidth; x++)
            {
                for (int y = 0; y < partHeight; y++)
                {
                    if (x + currentwidth >= size2X) { continue; }
                    if (y >= size2Y) { continue; }
                    int c = partBmpPixels[MapUtilCi.Index2d(x, y, partWidth)];
                    if (Game.ColorA(c) > 0)
                    {
                        bmp2Pixels[MapUtilCi.Index2d(platform.FloatToInt(currentwidth) + x, y, size2X)] = c;
                    }
                }
            }
            currentwidth += sizeiX;
        }
        platform.BitmapSetPixelsArgb(bmp2, bmp2Pixels);
        return bmp2;
    }

    public TextPart[] DecodeColors(string s, int defaultcolor, IntRef retLength)
    {
        TextPart[] parts = new TextPart[256];
        int partsCount = 0;
        int currentcolor = defaultcolor;
        int[] currenttext = new int[256];
        int currenttextLength = 0;
        IntRef sLength = new IntRef();
        int[] sChars = platform.StringToCharArray(s, sLength);
        for (int i = 0; i < sLength.value; i++)
        {
            // If a & is found, try to parse a color code
            if (sChars[i] == '&')
            {
                //check if there's a character after it
                if (i + 1 < sLength.value)
                {
                    //try to parse the color code
                    int color = HexToInt(sChars[i + 1]);
                    if (color != -1)
                    {
                        //Color has been parsed successfully
                        if (currenttextLength != 0)
                        {
                            //Add content so far to return value
                            TextPart part = new TextPart();
                            part.text = platform.CharArrayToString(currenttext, currenttextLength);
                            part.color = currentcolor;
                            parts[partsCount++] = part;
                        }
                        //Update current color and reset stored text
                        for (int k = 0; k < currenttextLength; k++)
                        {
                            currenttext[k] = 0;
                        }
                        currenttextLength = 0;
                        currentcolor = GetColor(color);
                        //Increment i to prevent the code from being read again
                        i++;
                    }
                    else
                    {
                        //no valid color code found. display as normal character
                        currenttext[currenttextLength++] = sChars[i];
                    }
                }
                else
                {
                    //if not, just display it as normal character
                    currenttext[currenttextLength++] = sChars[i];
                }
            }
            else
            {
                //Nothing special. Just add the current character
                currenttext[currenttextLength++] = s[i];
            }
        }
        //Add any leftover text parts in current color
        if (currenttextLength != 0)
        {
            TextPart part = new TextPart();
            part.text = platform.CharArrayToString(currenttext, currenttextLength);
            part.color = currentcolor;
            parts[partsCount++] = part;
        }
        retLength.value = partsCount;
        return parts;
    }

    int NextPowerOfTwo(int x)
    {
        x--;
        x |= x >> 1;  // handle  2 bit numbers
        x |= x >> 2;  // handle  4 bit numbers
        x |= x >> 4;  // handle  8 bit numbers
        x |= x >> 8;  // handle 16 bit numbers
        //x |= x >> 16; // handle 32 bit numbers
        x++;
        return x;
    }

    int GetColor(int currentcolor)
    {
        switch (currentcolor)
        {
            case 0: { return Game.ColorFromArgb(255, 0, 0, 0); }
            case 1: { return Game.ColorFromArgb(255, 0, 0, 191); }
            case 2: { return Game.ColorFromArgb(255, 0, 191, 0); }
            case 3: { return Game.ColorFromArgb(255, 0, 191, 191); }
            case 4: { return Game.ColorFromArgb(255, 191, 0, 0); }
            case 5: { return Game.ColorFromArgb(255, 191, 0, 191); }
            case 6: { return Game.ColorFromArgb(255, 191, 191, 0); }
            case 7: { return Game.ColorFromArgb(255, 191, 191, 191); }
            case 8: { return Game.ColorFromArgb(255, 40, 40, 40); }
            case 9: { return Game.ColorFromArgb(255, 64, 64, 255); }
            case 10: { return Game.ColorFromArgb(255, 64, 255, 64); }
            case 11: { return Game.ColorFromArgb(255, 64, 255, 255); }
            case 12: { return Game.ColorFromArgb(255, 255, 64, 64); }
            case 13: { return Game.ColorFromArgb(255, 255, 64, 255); }
            case 14: { return Game.ColorFromArgb(255, 255, 255, 64); }
            case 15: { return Game.ColorFromArgb(255, 255, 255, 255); }
            default: return Game.ColorFromArgb(255, 255, 255, 255);
        }
    }

    int HexToInt(int c)
    {
        if (c == '0') { return 0; }
        if (c == '1') { return 1; }
        if (c == '2') { return 2; }
        if (c == '3') { return 3; }
        if (c == '4') { return 4; }
        if (c == '5') { return 5; }
        if (c == '6') { return 6; }
        if (c == '7') { return 7; }
        if (c == '8') { return 8; }
        if (c == '9') { return 9; }
        if (c == 'a') { return 10; }
        if (c == 'b') { return 11; }
        if (c == 'c') { return 12; }
        if (c == 'd') { return 13; }
        if (c == 'e') { return 14; }
        if (c == 'f') { return 15; }
        return -1;
    }
}

public class CameraMove
{
    internal bool TurnLeft;
    internal bool TurnRight;
    internal bool DistanceUp;
    internal bool DistanceDown;
    internal bool AngleUp;
    internal bool AngleDown;
    internal int MoveX;
    internal int MoveY;
    internal float Distance;
}

public class Kamera
{
    public Kamera()
    {
        one = 1;
        distance = 5;
        Angle = 45;
        MinimumDistance = 2;
        tt = 0;
        MaximumAngle = 89;
        MinimumAngle = 0;
        Center = new Vector3Ref();
    }
    float one;
    public void GetPosition(GamePlatform platform, Vector3Ref ret)
    {
        float cx = platform.MathCos(tt * one / 2) * GetFlatDistance(platform) + Center.X;
        float cy = platform.MathSin(tt * one / 2) * GetFlatDistance(platform) + Center.Z;
        ret.X = cx;
        ret.Y = Center.Y + GetCameraHeightFromCenter(platform);
        ret.Z = cy;
    }
    float distance;
    public float GetDistance() { return distance; }
    public void SetDistance(float value)
    {
        distance = value;
        if (distance < MinimumDistance)
        {
            distance = MinimumDistance;
        }
    }
    internal float Angle;
    internal float MinimumDistance;
    float GetCameraHeightFromCenter(GamePlatform platform)
    {
        return platform.MathSin(Angle * Game.GetPi() / 180) * distance;
    }
    float GetFlatDistance(GamePlatform platform)
    {
        return platform.MathCos(Angle * Game.GetPi() / 180) * distance;
    }
    internal Vector3Ref Center;
    internal float tt;
    public float GetT()
    {
        return tt;
    }
    public void SetT(float value)
    {
        tt = value;
    }
    public void TurnLeft(float p)
    {
        tt += p;
    }
    public void TurnRight(float p)
    {
        tt -= p;
    }
    public void Move(CameraMove camera_move, float p)
    {
        p *= 2;
        p *= 2;
        if (camera_move.TurnLeft)
        {
            TurnLeft(p);
        }
        if (camera_move.TurnRight)
        {
            TurnRight(p);
        }
        if (camera_move.DistanceUp)
        {
            SetDistance(GetDistance() + p);
        }
        if (camera_move.DistanceDown)
        {
            SetDistance(GetDistance() - p);
        }
        if (camera_move.AngleUp)
        {
            Angle += p * 10;
        }
        if (camera_move.AngleDown)
        {
            Angle -= p * 10;
        }
        SetDistance(camera_move.Distance);
        //if (MaximumAngle < MinimumAngle) { throw new Exception(); }
        SetValidAngle();
    }

    void SetValidAngle()
    {
        if (Angle > MaximumAngle) { Angle = MaximumAngle; }
        if (Angle < MinimumAngle) { Angle = MinimumAngle; }
    }

    internal int MaximumAngle;
    internal int MinimumAngle;

    public float GetAngle()
    {
        return Angle;
    }

    public void SetAngle(float value)
    {
        Angle = value;
    }

    public void GetCenter(Vector3Ref ret)
    {
        ret.X = Center.X;
        ret.Y = Center.Y;
        ret.Z = Center.Z;
    }

    public void TurnUp(float p)
    {
        Angle += p;
        SetValidAngle();
    }
}

public abstract class IMapStorage2
{
    public abstract int GetMapSizeX();
    public abstract int GetMapSizeY();
    public abstract int GetMapSizeZ();
    public abstract int GetBlock(int x, int y, int z);
    public abstract void SetBlock(int x, int y, int z, int tileType);
}

public class MapStorage2 : IMapStorage2
{
    public static MapStorage2 Create(Game game)
    {
        MapStorage2 s = new MapStorage2();
        s.game = game;
        return s;
    }
    Game game;
    public override int GetMapSizeX()
    {
        return game.MapSizeX;
    }

    public override int GetMapSizeY()
    {
        return game.MapSizeY;
    }

    public override int GetMapSizeZ()
    {
        return game.MapSizeZ;
    }

    public override int GetBlock(int x, int y, int z)
    {
        return game.GetBlock(x, y, z);
    }

    public override void SetBlock(int x, int y, int z, int tileType)
    {
        game.SetBlock(x, y, z, tileType);
    }
}

public class GameDataMonsters
{
    public GameDataMonsters()
    {
        int n = 5;
        MonsterCode = new string[n];
        MonsterName = new string[n];
        MonsterSkin = new string[n];
        MonsterCode[0] = "imp.txt";
        MonsterName[0] = "Imp";
        MonsterSkin[0] = "imp.png";
        MonsterCode[1] = "imp.txt";
        MonsterName[1] = "Fire Imp";
        MonsterSkin[1] = "impfire.png";
        MonsterCode[2] = "dragon.txt";
        MonsterName[2] = "Dragon";
        MonsterSkin[2] = "dragon.png";
        MonsterCode[3] = "zombie.txt";
        MonsterName[3] = "Zombie";
        MonsterSkin[3] = "zombie.png";
        MonsterCode[4] = "cyclops.txt";
        MonsterName[4] = "Cyclops";
        MonsterSkin[4] = "cyclops.png";
    }
    internal string[] MonsterName;
    internal string[] MonsterCode;
    internal string[] MonsterSkin;
}

public enum GuiState
{
    Normal,
    EscapeMenu,
    Inventory,
    MapLoading,
    CraftingRecipes,
    ModalDialog
}

public enum BlockSetMode
{
    Destroy,
    Create,
    Use, //open doors, use crafting table, etc.
    UseWithTool
}

public enum FontType
{
    Nice,
    Simple,
    BlackBackground,
    Default
}

public class SpecialBlockId
{
    public const int Empty = 0;
}

public class GameData
{
    public GameData()
    {
        mBlockIdEmpty = 0;
        mBlockIdDirt = -1;
        mBlockIdSponge = -1;
        mBlockIdTrampoline = -1;
        mBlockIdAdminium = -1;
        mBlockIdCompass = -1;
        mBlockIdLadder = -1;
        mBlockIdEmptyHand = -1;
        mBlockIdCraftingTable = -1;
        mBlockIdLava = -1;
        mBlockIdStationaryLava = -1;
        mBlockIdFillStart = -1;
        mBlockIdCuboid = -1;
        mBlockIdFillArea = -1;
        mBlockIdMinecart = -1;
        mBlockIdRailstart = -128; // 64 rail tiles
    }
    public void Start()
    {
        Initialize(GlobalVar.MAX_BLOCKTYPES);
    }
    public void Update()
    {
    }
    void Initialize(int count)
    {
        mWhenPlayerPlacesGetsConvertedTo = new int[count];
        mIsFlower = new bool[count];
        mRail = new int[count];
        mWalkSpeed = new float[count];
        for (int i = 0; i < count; i++)
        {
            mWalkSpeed[i] = 1;
        }
        mIsSlipperyWalk = new bool[count];
        mWalkSound = new string[count][];
        for (int i = 0; i < count; i++)
        {
            mWalkSound[i] = new string[SoundCount];
        }
        mBreakSound = new string[count][];
        for (int i = 0; i < count; i++)
        {
            mBreakSound[i] = new string[SoundCount];
        }
        mBuildSound = new string[count][];
        for (int i = 0; i < count; i++)
        {
            mBuildSound[i] = new string[SoundCount];
        }
        mCloneSound = new string[count][];
        for (int i = 0; i < count; i++)
        {
            mCloneSound[i] = new string[SoundCount];
        }
        mLightRadius = new int[count];
        mStartInventoryAmount = new int[count];
        mStrength = new float[count];
        mDamageToPlayer = new int[count];
        mWalkableType = new int[count];

        mDefaultMaterialSlots = new int[10];
    }

    public int[] WhenPlayerPlacesGetsConvertedTo() { return mWhenPlayerPlacesGetsConvertedTo; }
    public bool[] IsFlower() { return mIsFlower; }
    public int[] Rail() { return mRail; }
    public float[] WalkSpeed() { return mWalkSpeed; }
    public bool[] IsSlipperyWalk() { return mIsSlipperyWalk; }
    public string[][] WalkSound() { return mWalkSound; }
    public string[][] BreakSound() { return mBreakSound; }
    public string[][] BuildSound() { return mBuildSound; }
    public string[][] CloneSound() { return mCloneSound; }
    public int[] LightRadius() { return mLightRadius; }
    public int[] StartInventoryAmount() { return mStartInventoryAmount; }
    public float[] Strength() { return mStrength; }
    public int[] DamageToPlayer() { return mDamageToPlayer; }
    public int[] WalkableType1() { return mWalkableType; }

    public int[] DefaultMaterialSlots() { return mDefaultMaterialSlots; }

    int[] mWhenPlayerPlacesGetsConvertedTo;
    bool[] mIsFlower;
    int[] mRail;
    float[] mWalkSpeed;
    bool[] mIsSlipperyWalk;
    string[][] mWalkSound;
    string[][] mBreakSound;
    string[][] mBuildSound;
    string[][] mCloneSound;
    int[] mLightRadius;
    int[] mStartInventoryAmount;
    float[] mStrength;
    int[] mDamageToPlayer;
    int[] mWalkableType;

    int[] mDefaultMaterialSlots;

    // TODO: hardcoded IDs
    // few code sections still expect some hardcoded IDs
    int mBlockIdEmpty;
    int mBlockIdDirt;
    int mBlockIdSponge;
    int mBlockIdTrampoline;
    int mBlockIdAdminium;
    int mBlockIdCompass;
    int mBlockIdLadder;
    int mBlockIdEmptyHand;
    int mBlockIdCraftingTable;
    int mBlockIdLava;
    int mBlockIdStationaryLava;
    int mBlockIdFillStart;
    int mBlockIdCuboid;
    int mBlockIdFillArea;
    int mBlockIdMinecart;
    int mBlockIdRailstart; // 64 rail tiles

    public int BlockIdEmpty() { return mBlockIdEmpty; }
    public int BlockIdDirt() { return mBlockIdDirt; }
    public int BlockIdSponge() { return mBlockIdSponge; }
    public int BlockIdTrampoline() { return mBlockIdTrampoline; }
    public int BlockIdAdminium() { return mBlockIdAdminium; }
    public int BlockIdCompass() { return mBlockIdCompass; }
    public int BlockIdLadder() { return mBlockIdLadder; }
    public int BlockIdEmptyHand() { return mBlockIdEmptyHand; }
    public int BlockIdCraftingTable() { return mBlockIdCraftingTable; }
    public int BlockIdLava() { return mBlockIdLava; }
    public int BlockIdStationaryLava() { return mBlockIdStationaryLava; }
    public int BlockIdFillStart() { return mBlockIdFillStart; }
    public int BlockIdCuboid() { return mBlockIdCuboid; }
    public int BlockIdFillArea() { return mBlockIdFillArea; }
    public int BlockIdMinecart() { return mBlockIdMinecart; }
    public int BlockIdRailstart() { return mBlockIdRailstart; }

    // TODO: atm it sets sepcial block id from block name - better use new block property
    public bool SetSpecialBlock(Packet_BlockType b, int id)
    {
        switch (b.Name)
        {
            case "Empty":
                this.mBlockIdEmpty = id;
                return true;
            case "Dirt":
                this.mBlockIdDirt = id;
                return true;
            case "Sponge":
                this.mBlockIdSponge = id;
                return true;
            case "Trampoline":
                this.mBlockIdTrampoline = id;
                return true;
            case "Adminium":
                this.mBlockIdAdminium = id;
                return true;
            case "Compass":
                this.mBlockIdCompass = id;
                return true;
            case "Ladder":
                this.mBlockIdLadder = id;
                return true;
            case "EmptyHand":
                this.mBlockIdEmptyHand = id;
                return true;
            case "CraftingTable":
                this.mBlockIdCraftingTable = id;
                return true;
            case "Lava":
                this.mBlockIdLava = id;
                return true;
            case "StationaryLava":
                this.mBlockIdStationaryLava = id;
                return true;
            case "FillStart":
                this.mBlockIdFillStart = id;
                return true;
            case "Cuboid":
                this.mBlockIdCuboid = id;
                return true;
            case "FillArea":
                this.mBlockIdFillArea = id;
                return true;
            case "Minecart":
                this.mBlockIdMinecart = id;
                return true;
            case "Rail0":
                this.mBlockIdRailstart = id;
                return true;
            default:
                return false;
        }
    }

    public bool IsRailTile(int id)
    {
        return id >= BlockIdRailstart() && id < BlockIdRailstart() + 64;
    }

    public void UseBlockTypes(GamePlatform platform, Packet_BlockType[] blocktypes, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (blocktypes[i] != null)
            {
                UseBlockType(platform, i, blocktypes[i]);
            }
        }
    }

    public void UseBlockType(GamePlatform platform, int id, Packet_BlockType b)
    {
        if (b.Name == null)//!b.IsValid)
        {
            return;
        }
        //public bool[] IsWater { get { return mIsWater; } }
        //            public bool[] IsTransparentForLight { get { return mIsTransparentForLight; } }
        //public bool[] IsEmptyForPhysics { get { return mIsEmptyForPhysics; } }

        if (b.WhenPlacedGetsConvertedTo != 0)
        {
            mWhenPlayerPlacesGetsConvertedTo[id] = b.WhenPlacedGetsConvertedTo;
        }
        else
        {
            mWhenPlayerPlacesGetsConvertedTo[id] = id;
        }
        IsFlower()[id] = b.DrawType == Packet_DrawTypeEnum.Plant;
        Rail()[id] = b.Rail;
        WalkSpeed()[id] = DeserializeFloat(b.WalkSpeedFloat);
        IsSlipperyWalk()[id] = b.IsSlipperyWalk;
        WalkSound()[id] = new string[SoundCount];
        BreakSound()[id] = new string[SoundCount];
        BuildSound()[id] = new string[SoundCount];
        CloneSound()[id] = new string[SoundCount];
        if (b.Sounds != null)
        {
            for (int i = 0; i < b.Sounds.WalkCount; i++)
            {
                WalkSound()[id][i] = StringTools.StringAppend(platform, b.Sounds.Walk[i], ".wav");
            }
            for (int i = 0; i < b.Sounds.Break1Count; i++)
            {
                BreakSound()[id][i] = StringTools.StringAppend(platform, b.Sounds.Break1[i], ".wav");
            }
            for (int i = 0; i < b.Sounds.BuildCount; i++)
            {
                BuildSound()[id][i] = StringTools.StringAppend(platform, b.Sounds.Build[i], ".wav");
            }
            for (int i = 0; i < b.Sounds.CloneCount; i++)
            {
                CloneSound()[id][i] = StringTools.StringAppend(platform, b.Sounds.Clone[i], ".wav");
            }
        }
        LightRadius()[id] = b.LightRadius;
        //StartInventoryAmount { get; }
        Strength()[id] = b.Strength;
        DamageToPlayer()[id] = b.DamageToPlayer;
        WalkableType1()[id] = b.WalkableType;
        SetSpecialBlock(b, id);
    }

    public const int SoundCount = 8;

    float DeserializeFloat(int p)
    {
        float one = 1;
        return (one * p) / 32;
    }
}

public class OnCrashHandlerLeave : OnCrashHandler
{
    public static OnCrashHandlerLeave Create(Game game)
    {
        OnCrashHandlerLeave oncrash = new OnCrashHandlerLeave();
        oncrash.g = game;
        return oncrash;
    }
    Game g;
    public override void OnCrash()
    {
        g.SendLeave(Packet_LeaveReasonEnum.Crash);
    }
}

public class OptionsCi
{
    public OptionsCi()
    {
        float one = 1;
        Shadows = false;
        Font = 0;
        DrawDistance = 32;
        UseServerTextures = true;
        EnableSound = true;
        EnableAutoJump = false;
        ClientLanguage = "";
        Framerate = 0;
        Resolution = 0;
        Fullscreen = false;
        Smoothshadows = true;
        BlockShadowSave = one * 6 / 10;
        Keys = new int[256];
    }
    internal bool Shadows;
    internal int Font;
    internal int DrawDistance;
    internal bool UseServerTextures;
    internal bool EnableSound;
    internal bool EnableAutoJump;
    internal string ClientLanguage;
    internal int Framerate;
    internal int Resolution;
    internal bool Fullscreen;
    internal bool Smoothshadows;
    internal float BlockShadowSave;
    internal int[] Keys;
}

public class ClientPackets
{
    public static Packet_Client CreateLoginPacket(GamePlatform platform, string username, string verificationKey)
    {
        Packet_ClientIdentification p = new Packet_ClientIdentification();
        {
            p.Username = username;
            p.MdProtocolVersion = platform.GetGameVersion();
            p.VerificationKey = verificationKey;
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.PlayerIdentification;
        pp.Identification = p;
        return pp;
    }

    public static Packet_Client CreateLoginPacket_(GamePlatform platform, string username, string verificationKey, string serverPassword)
    {
        Packet_ClientIdentification p = new Packet_ClientIdentification();
        {
            p.Username = username;
            p.MdProtocolVersion = platform.GetGameVersion();
            p.VerificationKey = verificationKey;
            p.ServerPassword = serverPassword;
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.PlayerIdentification;
        pp.Identification = p;
        return pp;
    }

    public static Packet_Client Oxygen(int currentOxygen)
    {
        Packet_Client packet = new Packet_Client();
        packet.Id = Packet_ClientIdEnum.Oxygen;
        packet.Oxygen = new Packet_ClientOxygen();
        packet.Oxygen.CurrentOxygen = currentOxygen;
        return packet;
    }

    public static Packet_Client Reload()
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.Reload;
        p.Reload = new Packet_ClientReload();
        return p;
    }

    public static Packet_Client Chat(string s, int isTeamchat)
    {
        Packet_ClientMessage p = new Packet_ClientMessage();
        p.Message = s;
        p.IsTeamchat = isTeamchat;
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.Message;
        pp.Message = p;
        return pp;
    }

    public static Packet_Client PingReply()
    {
        Packet_ClientPingReply p = new Packet_ClientPingReply();
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.PingReply;
        pp.PingReply = p;
        return pp;
    }

    public static Packet_Client SetBlock(int x, int y, int z, int mode, int type, int materialslot)
    {
        Packet_ClientSetBlock p = new Packet_ClientSetBlock();
        {
            p.X = x;
            p.Y = y;
            p.Z = z;
            p.Mode = mode;
            p.BlockType = type;
            p.MaterialSlot = materialslot;
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.SetBlock;
        pp.SetBlock = p;
        return pp;
    }

    public static Packet_Client SpecialKeyRespawn()
    {
        Packet_Client p = new Packet_Client();
        {
            p.Id = Packet_ClientIdEnum.SpecialKey;
            p.SpecialKey_ = new Packet_ClientSpecialKey();
            p.SpecialKey_.Key_ = Packet_SpecialKeyEnum.Respawn;
        }
        return p;
    }

    public static Packet_Client FillArea(int startx, int starty, int startz, int endx, int endy, int endz, int blockType, int ActiveMaterial)
    {
        Packet_ClientFillArea p = new Packet_ClientFillArea();
        {
            p.X1 = startx;
            p.Y1 = starty;
            p.Z1 = startz;
            p.X2 = endx;
            p.Y2 = endy;
            p.Z2 = endz;
            p.BlockType = blockType;
            p.MaterialSlot = ActiveMaterial;
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.FillArea;
        pp.FillArea = p;
        return pp;
    }

    public static Packet_Client InventoryClick(Packet_InventoryPosition pos)
    {
        Packet_ClientInventoryAction p = new Packet_ClientInventoryAction();
        p.A = pos;
        p.Action = Packet_InventoryActionTypeEnum.Click;
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.InventoryAction;
        pp.InventoryAction = p;
        return pp;
    }

    public static Packet_Client WearItem(Packet_InventoryPosition from, Packet_InventoryPosition to)
    {
        Packet_ClientInventoryAction p = new Packet_ClientInventoryAction();
        p.A = from;
        p.B = to;
        p.Action = Packet_InventoryActionTypeEnum.WearItem;
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.InventoryAction;
        pp.InventoryAction = p;
        return pp;
    }

    public static Packet_Client MoveToInventory(Packet_InventoryPosition from)
    {
        Packet_ClientInventoryAction p = new Packet_ClientInventoryAction();
        p.A = from;
        p.Action = Packet_InventoryActionTypeEnum.MoveToInventory;
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.InventoryAction;
        pp.InventoryAction = p;
        return pp;
    }

    public static Packet_Client Death(int reason, int sourcePlayer)
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.Death;
        p.Death = new Packet_ClientDeath();
        {
            p.Death.Reason = reason;
            p.Death.SourcePlayer = sourcePlayer;
        }
        return p;
    }

    public static Packet_Client Health(int currentHealth)
    {
        Packet_Client p = new Packet_Client();
        {
            p.Id = Packet_ClientIdEnum.Health;
            p.Health = new Packet_ClientHealth();
            p.Health.CurrentHealth = currentHealth;
        }
        return p;
    }

    public static Packet_Client RequestBlob(Game game, string[] required, int requiredCount)
    {
        Packet_ClientRequestBlob p = new Packet_ClientRequestBlob(); //{ RequestBlobMd5 = needed };
        if (game.ServerVersionAtLeast(2014, 4, 13))
        {
            p.RequestedMd5 = new Packet_StringList();
            p.RequestedMd5.SetItems(required, requiredCount, requiredCount);
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.RequestBlob;
        pp.RequestBlob = p;
        return pp;
    }

    public static Packet_Client Leave(int reason)
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.Leave;
        p.Leave = new Packet_ClientLeave();
        p.Leave.Reason = reason;
        return p;
    }

    public static Packet_Client Craft(int x, int y, int z, int recipeId)
    {
        Packet_ClientCraft cmd = new Packet_ClientCraft();
        cmd.X = x;
        cmd.Y = y;
        cmd.Z = z;
        cmd.RecipeId = recipeId;
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.Craft;
        p.Craft = cmd;
        return p;
    }

    public static Packet_Client DialogClick(string widgetId, string[] textValues, int textValuesCount)
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.DialogClick;
        p.DialogClick_ = new Packet_ClientDialogClick();
        p.DialogClick_.WidgetId = widgetId;
        p.DialogClick_.SetTextBoxValue(textValues, textValuesCount, textValuesCount);
        return p;
    }

    public static Packet_Client GameResolution(int width, int height)
    {
        Packet_ClientGameResolution p = new Packet_ClientGameResolution();
        p.Width = width;
        p.Height = height;
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.GameResolution;
        pp.GameResolution = p;
        return pp;
    }

    public static Packet_Client SpecialKeyTabPlayerList()
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.SpecialKey;
        p.SpecialKey_ = new Packet_ClientSpecialKey();
        p.SpecialKey_.Key_ = Packet_SpecialKeyEnum.TabPlayerList;
        return p;
    }

    public static Packet_Client SpecialKeySelectTeam()
    {
        Packet_Client p = new Packet_Client();
        {
            p.Id = Packet_ClientIdEnum.SpecialKey;
            p.SpecialKey_ = new Packet_ClientSpecialKey();
            p.SpecialKey_.Key_ = Packet_SpecialKeyEnum.SelectTeam;
        }
        return p;
    }

    public static Packet_Client SpecialKeySetSpawn()
    {
        Packet_Client p = new Packet_Client();
        {
            p.Id = Packet_ClientIdEnum.SpecialKey;
            p.SpecialKey_ = new Packet_ClientSpecialKey();
            p.SpecialKey_.Key_ = Packet_SpecialKeyEnum.SetSpawn;
        }
        return p;
    }

    public static Packet_Client ActiveMaterialSlot(int ActiveMaterial)
    {
        Packet_Client p = new Packet_Client();
        {
            p.Id = Packet_ClientIdEnum.ActiveMaterialSlot;
            p.ActiveMaterialSlot = new Packet_ClientActiveMaterialSlot();
            p.ActiveMaterialSlot.ActiveMaterialSlot = ActiveMaterial;
        }
        return p;
    }

    public static Packet_Client MonsterHit(int damage)
    {
        Packet_ClientHealth p = new Packet_ClientHealth();
        p.CurrentHealth = damage;
        Packet_Client packet = new Packet_Client();
        packet.Id = Packet_ClientIdEnum.MonsterHit;
        packet.Health = p;
        return packet;
    }

    public static Packet_Client PositionAndOrientation(Game game, int playerId, float positionX, float positionY, float positionZ, float orientationX, float orientationY, float orientationZ, byte stance)
    {
        Packet_ClientPositionAndOrientation p = new Packet_ClientPositionAndOrientation();
        {
            p.PlayerId = playerId;
            p.X = game.platform.FloatToInt(positionX * 32);
            p.Y = game.platform.FloatToInt(positionY * 32);
            p.Z = game.platform.FloatToInt(positionZ * 32);
            p.Heading = game.platform.FloatToInt(Game.RadToAngle256(orientationY));
            p.Pitch = game.platform.FloatToInt(Game.RadToAngle256(orientationX));
            p.Stance = stance;
        }
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.PositionandOrientation;
        pp.PositionAndOrientation = p;
        return pp;
    }

    public static Packet_Client ServerQuery()
    {
        Packet_ClientServerQuery p1 = new Packet_ClientServerQuery();
        Packet_Client pp = new Packet_Client();
        pp.Id = Packet_ClientIdEnum.ServerQuery;
        pp.Query = p1;
        return pp;
    }

    internal static Packet_Client UseEntity(int entityId)
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.EntityInteraction;
        p.EntityInteraction = new Packet_ClientEntityInteraction();
        p.EntityInteraction.EntityId = entityId;
        p.EntityInteraction.InteractionType = Packet_EntityInteractionTypeEnum.Use;
        return p;
    }

    internal static Packet_Client HitEntity(int entityId)
    {
        Packet_Client p = new Packet_Client();
        p.Id = Packet_ClientIdEnum.EntityInteraction;
        p.EntityInteraction = new Packet_ClientEntityInteraction();
        p.EntityInteraction.EntityId = entityId;
        p.EntityInteraction.InteractionType = Packet_EntityInteractionTypeEnum.Hit;
        return p;
    }
}

public class ServerPackets
{
    public static Packet_Server Message(string text)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.Message;
        p.Message = new Packet_ServerMessage();
        p.Message.Message = text;
        return p;
    }

    public static Packet_Server LevelInitialize()
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.LevelInitialize;
        p.LevelInitialize = new Packet_ServerLevelInitialize();
        return p;
    }

    public static Packet_Server LevelFinalize()
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.LevelFinalize;
        p.LevelFinalize = new Packet_ServerLevelFinalize();
        return p;
    }

    public static Packet_Server Identification(int assignedClientId, int mapSizeX, int mapSizeY, int mapSizeZ, string version)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.ServerIdentification;
        p.Identification = new Packet_ServerIdentification();
        p.Identification.AssignedClientId = assignedClientId;
        p.Identification.MapSizeX = mapSizeX;
        p.Identification.MapSizeY = mapSizeY;
        p.Identification.MapSizeZ = mapSizeZ;
        p.Identification.ServerName = "Simple";
        p.Identification.MdProtocolVersion = version;
        return p;
    }
    public static byte[] Serialize(Packet_Server packet, IntRef retLength)
    {
        CitoMemoryStream ms = new CitoMemoryStream();
        Packet_ServerSerializer.Serialize(ms, packet);
        byte[] data = ms.ToArray();
        retLength.value = ms.Length();
        return data;
    }

    public static Packet_Server BlockType(int id, Packet_BlockType blockType)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.BlockType;
        p.BlockType = new Packet_ServerBlockType();
        p.BlockType.Id = id;
        p.BlockType.Blocktype = blockType;
        return p;
    }

    public static Packet_Server BlockTypes()
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.BlockTypes;
        p.BlockTypes = new Packet_ServerBlockTypes();
        return p;
    }

    public static Packet_Server Chunk_(int x, int y, int z, int chunksize)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.Chunk_;
        p.Chunk_ = new Packet_ServerChunk();
        p.Chunk_.X = x;
        p.Chunk_.Y = y;
        p.Chunk_.Z = z;
        p.Chunk_.SizeX = chunksize;
        p.Chunk_.SizeY = chunksize;
        p.Chunk_.SizeZ = chunksize;
        return p;
    }

    public static Packet_Server ChunkPart(byte[] compressedChunkPart)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.ChunkPart;
        p.ChunkPart = new Packet_ServerChunkPart();
        p.ChunkPart.CompressedChunkPart = compressedChunkPart;
        return p;
    }

    internal static Packet_Server SetBlock(int x, int y, int z, int block)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.SetBlock;
        p.SetBlock = new Packet_ServerSetBlock();
        p.SetBlock.X = x;
        p.SetBlock.Y = y;
        p.SetBlock.Z = z;
        p.SetBlock.BlockType = block;
        return p;
    }

    internal static Packet_Server PlayerStats(int health, int maxHealth, int oxygen, int maxOxygen)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.PlayerStats;
        p.PlayerStats = new Packet_ServerPlayerStats();
        p.PlayerStats.CurrentHealth = health;
        p.PlayerStats.MaxHealth = maxHealth;
        p.PlayerStats.CurrentOxygen = oxygen;
        p.PlayerStats.MaxOxygen = maxOxygen;
        return p;
    }

    internal static Packet_Server Inventory(Packet_Inventory inventory)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.FiniteInventory;
        p.Inventory = new Packet_ServerInventory();
        p.Inventory.Inventory = inventory;
        return p;
    }

    internal static Packet_Server Ping()
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.Ping;
        p.Ping = new Packet_ServerPing();
        return p;
    }

    internal static Packet_Server DisconnectPlayer(string disconnectReason)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.DisconnectPlayer;
        p.DisconnectPlayer = new Packet_ServerDisconnectPlayer();
        p.DisconnectPlayer.DisconnectReason = disconnectReason;
        return p;
    }
    
    internal static Packet_Server AnswerQuery(Packet_ServerQueryAnswer answer)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.QueryAnswer;
        p.QueryAnswer = answer;
        return p;
    }

    internal static Packet_Server EntitySpawn(int id, Packet_ServerEntity entity)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.EntitySpawn;
        p.EntitySpawn = new Packet_ServerEntitySpawn();
        p.EntitySpawn.Id = id;
        p.EntitySpawn.Entity_ = entity;
        return p;
    }

    internal static Packet_Server EntityPositionAndOrientation(int id, Packet_PositionAndOrientation positionAndOrientation)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.EntityPosition;
        p.EntityPosition = new Packet_ServerEntityPositionAndOrientation();
        p.EntityPosition.Id = id;
        p.EntityPosition.PositionAndOrientation = positionAndOrientation;
        return p;
    }

    internal static Packet_Server EntityDespawn(int id)
    {
        Packet_Server p = new Packet_Server();
        p.Id = Packet_ServerIdEnum.EntityDespawn;
        p.EntityDespawn = new Packet_ServerEntityDespawn();
        p.EntityDespawn.Id = id;
        return p;
    }
}

public class TextureAtlas
{
    public static RectFRef TextureCoords2d(int textureId, int texturesPacked)
    {
        float one = 1;
        RectFRef r = new RectFRef();
        r.y = (one / texturesPacked * (textureId / texturesPacked));
        r.x = (one / texturesPacked * (textureId % texturesPacked));
        r.w = one / texturesPacked;
        r.h = one / texturesPacked;
        return r;
    }
}

public abstract class ClientPacketHandler
{
    public ClientPacketHandler()
    {
        one = 1;
    }
    internal float one;
    public abstract void Handle(Game game, Packet_Server packet);
}
