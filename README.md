# QqGroupSummary
基于SoruxBot的QQ群信息整理，可以提供消息概括，话题检测后私发等省流功能的机器人插件。

# 使用

本插件基于SoruxBot开发，在使用前你需要使用SoruxBot框架来加载本插件。  

本插件无特别的配置流程，在启动SoruxBot框架后即可正常使用。  

版本限制：

QqGroupSummary基于SoruxBot Beta v1.0.3，由于SoruxBot破坏性更新，不兼容低版本SoruxBot。

# 插件命令

- 插件命令均以`#`开头，且插件命令只能在群聊中使用。  

```
//添加话题
#topic [话题内容] [QQ] [QQ群号] [enable/disable]
//以上命令为，向某个群添加某个QQ对某个话题的检测：
//e.g. #topic 原神 1919810 114514 enable
```

```
//总结
#summary
//得到最近一段时间内群消息的总结内容
```

```
//添加Token
#summarytokenset [token]
//添加GptToken
```

# 可扩展性

在插件目录`summary`下的配置文件中可以找到插件的配置文件，编辑配置文件，使`remoteServer`选项为`enable`后，可以配置上报服务器。

配置模板：

请注意，由于SoruxBot手动Hash了配置文件，你可能需要在`Data\b79606fb3afea5bd1609ed40b622142f1c98125abcfe89a76a661b0e8e343910.json`中修改配置

```json
{
    "enableServer":"true",
    "endPoint":"https://localhost:8080"
}
```

上报：

```json
POST /summary/TopicLister
{
	"Topic":"话题",
    "Group":"触发的群",
    "User":"触发的用户",
    "RawText":"消息原文",
    "Summary":"总结的内容"
}
```

```json
POST /summary/Summary
{
    "Group":"触发的群",
    "RawText":"消息原文",
    "Summary":"总结的内容"
}
```

# 权限节点

在插件中，对于话题的添加和设置GptToken需要权限节点`EpicMo.Plugins.QqGroupSummary.SetTopic`和`EpicMo.Plugins.QqGroupSummary.SetToken`
