# 一、总体概念

## **1.** **游戏名称**

《不死人的奇妙冒险》

## **2.** **游戏背景**

《不死人的奇妙冒险》是由本小组自主研发的一款全新箱庭世界冒险游戏。游戏发生在一个被称作「水晶域」的幻想世界，在这里，被水晶选中的人将被授予「传送与复活之力」。你将扮演一位失去记忆的「外来者」，在旅行中邂逅一位同行的少女，一起击败强敌，找回失散的记忆——同时，逐步发掘「不死人」的真相。

## **3.** **游戏功能划分**

本游戏是一款模仿《黑暗之魂》设计的箱庭探索游戏，实现了游戏所需的绝大部分功能。具体的游戏设计分为地图关卡系统、敌人战斗系统、玩家移动武器系统、UI交互与数值系统等。

本人负责了其中的地图关卡系统部分。

# **二、关卡设计**

## **1.** **地图编辑**

游戏一共3个场景，分别为：山中村落、破败教堂、王国城堡

由于使用的都是免费资源，尽量保证了每一个大场景中的风格保持一致。三个场景布置在同一张Terrain上。整体的地图区域布局如下图所示。

![img](D:\Download\Typora\images\README\clip_image002.png)

下面的图片是地图各区域的概览图：

![img](D:\Download\Typora\images\README\clip_image004.png)![img](D:\Download\Typora\images\README\clip_image006.png)![img](D:\Download\Typora\images\README\clip_image008.png)![img](D:\Download\Typora\images\README\clip_image010.png)![img](D:\Download\Typora\images\README\clip_image012.png)![img](D:\Download\Typora\images\README\clip_image014.png)

地图功能划分上，第一个地图“山中村落”是新手教学关，可以让刚进入游戏的玩家熟悉游戏的战斗、探索等系统，快速上手游戏；第二个地图“破败教堂”是游戏的中枢地图，起到联系各个箱庭地图关卡的作用。第三个地图“王国城堡”是困难一些的关卡，用来考验玩家的反应能力、探索细致程度等。

## **2.** **机关设置**

游戏场景中需要编程实现的简易机关有：水晶（复活点）、宝箱、门、电梯，以及可交互NPC。

 

### (1)水晶

水晶在游戏中起到复活点的作用。靠近水晶弹出按键提示，按下F后水晶点亮，再次按下F打开传送面板，选择水晶名称并传送，在玩家死后，可以在上一个点亮的水晶处复活

**![img](D:\Download\Typora\images\README\clip_image016.png)**  ![img](D:\Download\Typora\images\README\clip_image018.png)

 

### (2)宝箱

靠近弹出按键提示，按下F后播放打开动画，然后显示UI，提示玩家获得宝箱内容物。宝箱打开3秒后消失，在背包中可以看到获得的物品，同时获得的武器也可以被切换到。

 

### (3)门

靠近弹出按键提示，按下F后播放开门动画。打开一次以后就不能再被交互。有的门需要钥匙，先从宝箱中获得钥匙才能开门。直接开门的话，会显示提示需要钥匙。

![img](D:\Download\Typora\images\README\clip_image020.png) ![img](D:\Download\Typora\images\README\clip_image022.png)

 

### **(4)**电梯

由一块踏板、上下两个拉杆组成。初始位于低位，踩下踏板开始上升，在上部踩下踏板开始下降。电梯在高处时下方拉杆激活，拉动下方拉杆，电梯下降，反之亦然

![img](D:\Download\Typora\images\README\clip_image024.png) ![img](D:\Download\Typora\images\README\clip_image026.png)

 

### **(5)NPC**

来自异世界的神秘少女，伴随玩家一同前进，靠近按下F对话，打开对话后按下空格键显示下一句对话。对话结束后还可以重新进入对话，但是结束后只能说一句话、

 

## **3.** **宝箱布置**

在场景各部分放置了宝箱，宝箱中有武器、药水和钥匙，有的宝箱藏在角落里，有的需要迂回拿到。

![img](D:\Download\Typora\images\README\clip_image028.png) ![img](D:\Download\Typora\images\README\clip_image030.png)

## **4.** **敌人布置**

敌人放置在了行进路线周围。在放置敌人的过程中，使用了一些小陷阱，用来增加游戏趣味。

![img](D:\Download\Typora\images\README\clip_image032.png)

# **三、动作和武器系统**

在本项目中，玩家一共可以使用六种武器：短剑、狼牙棒、长剑、戟、长斧、空手，以及一种防具盾牌：

 

![img](D:\Download\Typora\images\README\clip_image034.jpg) ![img](D:\Download\Typora\images\README\clip_image036.jpg) ![img](D:\Download\Typora\images\README\clip_image038.jpg)

![img](D:\Download\Typora\images\README\clip_image040.jpg) ![img](D:\Download\Typora\images\README\clip_image042.jpg)  ![img](D:\Download\Typora\images\README\clip_image044.jpg)

 

在武器攻击方式和特性上，短剑、狼牙棒和长剑使用的是一套攻击动画，归类为轻武器；长斧和戟是一套，归类为重武器；空手攻击是单独的一套。

 

## **1.** **攻击方式**

每种武器有三种攻击方式：普通攻击、重攻击、跑攻，普通攻击拥有一般的伤害并且消耗较少的耐力，跑攻拥有较高的伤害且消耗较多的耐力，而重攻击可以造成大量的伤害但也要消耗大量的耐力。游戏中的设定是跑攻的伤害和耐力消耗均为普通攻击的1.5倍，重攻击的伤害和耐力消耗均为普通攻击的两倍。

 

## **2.** **轻武器**

轻武器有较快的攻击速度，但是攻击范围较小、造成的伤害较少，消耗的耐力值也普遍较少。

在轻武器中，短剑是玩家一开始就拥有的，伤害一般，攻击范围小，但耐力消耗少，拥有较强的平衡性，很适合作为入门武器。游戏中短剑的基础伤害为15，基础耐力消耗为15。

狼牙棒是玩家可以在宝箱中发现的武器，伤害较短剑来说要高一点，但同时会消耗比较多的耐力，对于攻击节奏掌握不好的玩家来说会比较难掌控，适合对本战斗系统有一定熟练度的玩家使用。游戏中狼牙棒的基础伤害为20，基础耐力消耗为20。

长剑作为最顶尖的轻武器，拥有优秀的攻击范围，平衡的伤害和耐力消耗值，很适合作为玩家的毕业武器。游戏中长剑的基础伤害为25，基础耐力消耗为25。

 

## **3.** **重武器**

重武器拥有极大的攻击范围和攻击伤害，但同时每一次攻击都会消耗玩家大量的耐力，因此在使用重武器时，玩家需要掌握好时机，不能浪费每一次的攻击。

长斧是基础的重武器，伤害高、攻击范围大、攻速慢、消耗耐力多。游戏中巨斧的基础伤害为35，基础耐力消耗为35。

戟作为进阶重武器，在耐力消耗和攻速上进行了改进。游戏中戟的基础伤害为35，基础耐力消耗为30。

 

## **4.** **空手攻击**

空手攻击也是极具特色的攻击方式之一。尽管攻击范围小、造成的伤害不高，但是具有极快的攻击速度，损失的耐力值也十分少，机动性高，空手攻击玩家也是在游戏一开始就能够使用。游戏中空手攻击的基础伤害为10，基础耐力消耗为10。

 

## **5.** **防具**

盾牌在本游戏中也有着重要的作用。通过格挡，你可以减少敌人对你造成的伤害，游戏中的设定是格挡时玩家受到的伤害变为50% ；通过弹反，你可以免受伤害而反过来对敌人造成伤害，即当玩家处于弹反状态的那几帧内可以无视所有伤害。

 

## **6.** **其他动作**

除了武器系统，游戏中玩家还能够进行跑、跳、翻滚、上步等动作。跑使得玩家移动速度更快，跳使得玩家能够越过障碍物，翻滚在战斗中躲避攻击并且免受伤害，上步使得玩家能够快速与敌人拉开距离。

 

 

## **7.** **键位说明**

W/S/A/D    前后左右移动

鼠标左键     普通攻击

shift+鼠标左键  重攻击

space+鼠标左键  跑步攻击

鼠标右键(长按)  举盾格挡

space(长按)   奔跑

Q        弹反

E        跳跃

alt       翻滚

ctrl       上步

F        互动

R        恢复血量

鼠标滚轮向下   切换武器

B        打开背包

M       打开游戏内菜单

 

# **四、怪物系统**

因为项目类型是一块类似魂类游戏，结合地图风格选择了两款免费模型来搭配场景风格，在本项目中，当前一共有使用两种怪物：普通骷髅怪物和精英牛头怪物。

![img](D:\Download\Typora\images\README\clip_image046.png)![img](D:\Download\Typora\images\README\clip_image048.png)

骷髅怪的属性比较低，攻击低血量低，使得玩家可以快速击败骷髅怪，其用途主要为引导玩家按照路径移动、培养玩家的作战技巧以及在一些场景中骚扰和袭击玩家，在骚扰的同时不至于对玩家有太大的威胁，但是在玩家不注意的时候，突然的袭击还是有可能会击败玩家。

牛头怪的属性值比较高，和玩家属性比较类似，攻击力高血量也高，但是攻击速度较慢，玩家可以在攻击间隔之中拉开距离或者夹杂攻击，玩家在和牛头怪战斗时需要十分注意占位和攻击频率，主要作用是给玩家的闯关增加困难、指引宝箱位置、充当守关守卫。有和玩家1v1的能力，需要玩家使用之前学到的战斗技巧来应对。

![img](D:\Download\Typora\images\README\clip_image050.png) ![img](D:\Download\Typora\images\README\clip_image052.png)

怪物系统同时结合武器系统，武器和怪物躯干分开配置，攻击力数据也可以从武器中读取。同时也可以替换怪物使用的武器，在死亡时可以做到武器掉落供玩家拾取。同时怪物死亡时掉落经验提供给玩家，用于升级，玩家升级时怪物属性也会得到提升，用于平衡游戏难度。

 

# **五、UI和数值设计**

## **1.** **游戏UI**

游戏UI有以下几个部分。

![img](D:\Download\Typora\images\README\clip_image054.png)

本游戏是一个魂系风格的游戏，所以在选择UI素材时也尽量契合这一特点，在主页面中使用羊皮纸背景和红宝石配色，风格上比较匹配。在游戏开始时将会出现主菜单，这个菜单放置在游戏主场景之外。“开始游戏”按键将会加载一个新游戏；“玩法介绍”按键跳转到帮助页面；“退出游戏”按键则退出游戏。

![img](D:\Download\Typora\images\README\clip_image056.png)

在游戏中按M键唤醒游戏菜单，游戏菜单和主菜单功能大体相同，区别是，在主菜单中开始游戏将加载一个新游戏，而在游戏菜单中游戏开始将会从当前游戏存档最近点亮的复活水晶处复活。

![img](D:\Download\Typora\images\README\clip_image058.png)

选择玩法介绍将会出现帮助页面，帮助页面将介绍游戏中各种操作所绑定的键位。

![img](D:\Download\Typora\images\README\clip_image060.png)

背包是一个九宫格的样式，因而容量受到限制。在游戏中，玩家会通过击杀怪物和开宝箱等方式获取不同的武器、血瓶和钥匙，在背包中可以查看这些物品，如果消耗了血瓶这类资源，它将从背包中消失。

![img](D:\Download\Typora\images\README\clip_image062.png)

游戏中的常驻UI就是在左上角显示的玩家血条、耐力条和经验值条。玩家血条显示玩家当前生命值的比例，收到怪物攻击将减少，使用血瓶可以回复。经验值条记录玩家通过击杀怪物和开宝箱所获得的经验值，这是玩家等级系统的一部分。耐力值条反映了玩家当前耐力值，在玩家处于走或跑状态时会随时间回复，在玩家使用不同攻击动作时消耗相应值。

![img](D:\Download\Typora\images\README\clip_image064.png)

 

## **2.** **游戏数值**

游戏数值包括设定玩家和怪物的强度。对于玩家而言，可以设置的参数有：普攻伤害、重击伤害、普攻耐力消耗、重击耐力消耗、武器附加伤害、护盾效果等。玩家可以通过怪物击杀和打开宝箱获取经验值来升级。

玩家升级的效果是，血量、耐力、攻击、防御数值的提升。玩家强度增长在比例上先快后慢，在游戏前期给玩家容易上手的感觉，后期给玩家以挑战，怪物强度随之提升，相当于世界等级。下图显示了玩家和怪物的对战场景。

![img](D:\Download\Typora\images\README\clip_image066.png)

魂系游戏的一个特点就是游戏难度较大，考验玩家操作。在怪物数值方面主要是血量，攻击和攻速参数可以调整，预期设计是，轻武器怪：在5-10次攻击内击杀，在5-10次攻击击杀玩家，攻速中等；重武器怪：在5-10次攻击内击杀， 在3-5次攻击击杀玩家，攻速慢；弓箭怪：在2-5次攻击内击杀，在3-5次攻击击杀玩家,攻速中等。设定除boss外怪物强度与玩家级别同步增长，而boss到后期击杀难度降低，这样给玩家以强度提升的直观感受。



# 附录

游戏场景部分的代码和资源保存在`Assets/Scripts`和`Assets/My Essensial Assests`目录下

人物动作的代码和资源在`Assets/StarterAssets`目录下

敌人系统的代码和资源在`Assets/Enemies`目录下

UI部分的代码和资源在`Assets/UI`, `Assests/Scripts`和`Assests/`目录下



可直接运行的exe文件为`Build/Game Environments.exe`

