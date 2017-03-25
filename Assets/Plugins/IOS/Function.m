//
//  Function.m
//  Unity-iPhone
//
//  Created by Ambition on 2017/3/25.
//
//

#import <Foundation/Foundation.h>
#import "UnityAppController.h"

extern void OpenGame(const char *url)
{
    //获取Unity rootviewcontroller
    UIViewController *unityRootVC = UnityGetGLViewController();
    UIView *unityView = UnityGetGLView();
    
    NSString* urlStr = [[NSString alloc] initWithUTF8String:url];
    urlStr = [urlStr stringByAppendingString:@"://"];
    NSURL* nsUrl = [NSURL URLWithString:urlStr];
    
    NSLog(@"%@",urlStr);
    
    if ([[UIApplication sharedApplication] canOpenURL:nsUrl]){
        [[UIApplication sharedApplication] openURL:nsUrl];
    } else {
        NSLog(@"没有安装");
    }
    
}

