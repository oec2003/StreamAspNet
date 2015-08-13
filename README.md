## 开源上传组件stream的.Net后台实现

> `stream`上传组件主页：[http://www.twinkling.cn/](http://www.twinkling.cn/)
> `stream	`上传组件项目地址：[http://git.oschina.net/jiangdx/stream](http://git.oschina.net/jiangdx/stream)

本项目是	`stream`组件的.Net后台实现，目前只实现了Html5的版本。

### 项目结构图如下：
![enter image description here](http://ww1.sinaimg.cn/mw690/3cefded1gw1ev09rbskuoj20880eeab8.jpg)

1. **common：** 一些公共的帮助类和实体类
2. **css：** `stream`的`css`文件和图片
3. **js：** `stream`的`js`文件
4. **lib：** 第三方`dll`
5. **swf：** `stream`的`flash`文件
6. **upload：** 存放上传的文件和`tokens`
7. **FileUpload：** 一般处理程序，用来进行文件的操作

### 运行
1. 使用`VS2012`或`VS2013`打开项目，直接运行
2. 将程序部署在`IIS`中进行访问

### 运行效果
![enter image description here](http://ww2.sinaimg.cn/mw690/3cefded1gw1ev0a2g8kg7j20h90fhabc.jpg)
 
