using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Prime31;

public class iOsEtceteraController : MonoBehaviour {
	public string ITunesAppStoreId;
	
	void Start(){

	}

	
	// Takes a screenshot and puts it in the Application.persistentDataPath directory (which is Documents on iOS)
	// Optional completion handler provides the path to the image.
	public static IEnumerator TakeScreenShot(string filename){
		return EtceteraBinding.takeScreenShot( filename );
	}
		
	public static IEnumerator TakeScreenShot( string filename, Action<string> completionHandler ) {
		return EtceteraBinding.takeScreenShot( filename, completionHandler );
	}
			
	public static IEnumerator GetScreenShotTexture(Action<Texture2D> completionHandler ){
		return EtceteraBinding.getScreenShotTexture(completionHandler);
	}
			
	// Returns whether the application can open the url
	public static bool CanOpenUrl(string url){
		return EtceteraBinding.applicationCanOpenUrl(url);
	}
			
	// Returns the locale currently set on the device
	public static string GetCurrentLanguage(){
		return EtceteraBinding.getCurrentLanguage();
	}
			
	// Wraps the NSLocale objectForKey method. Passing true for useAutoUpdatingLocale will use the autoupdatingCurrentLocale, false will use the currentLocale
	// Some useful keys to request are kCFLocaleCurrencySymbolKey and kCFLocaleCountryCodeKey
	public static string GetLocalObjectForKey(bool useAutoUpdatingLocale, string key){
		return EtceteraBinding.localeObjectForKey( useAutoUpdatingLocale,key );
	}
			
	// Uses the Xcode Localizable.strings system to get a localized version of the given string
	public static string GetLocalizedString( string key, string defaultValue ){
		return EtceteraBinding.getLocalizedString(key, defaultValue);
	}
	// Deprecated		
	//public static void ShowAlertWithTitleAndButton( string title, string message, string buttonTitle ){
	//	EtceteraBinding.showAlertWithTitleMessageAndButton(title, message, buttonTitle);
	//}
			
	// Shows a standard Apple alert with the given title, message and an array of buttons. At least one button must be included.
	public static void ShowAlertWithTitleAndButtons( string title, string message, string[] buttons ){
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title, message, buttons);
	}
			
	// Shows a prompt with one text field
	public static void ShowPromptWithOneField( string title, string message, string placeHolder, bool autocomplete ){
		EtceteraBinding.showPromptWithOneField(title, message,placeHolder, autocomplete );
	}

	public void ShowLoginPrompt( string title, string message){
		ShowPromptWithTwoFields(title, message, "Username", "Password", false );
	}
	// Shows a prompt with two text fields
	public static void ShowPromptWithTwoFields( string title, string message, string placeHolder1, string placeHolder2, bool autocomplete ){
		EtceteraBinding.showPromptWithTwoFields(title, message,placeHolder1,placeHolder2, autocomplete);
	}
			
	// Opens a web view with the url (Url can either be a resource on the web or a local file) and optional controls (back, forward, copy, open in Safari)
	public static void ShowWebPage( string url, bool showControls ){
		EtceteraBinding.showWebPage(url,showControls);
	}
			
	// Checks to see if an email account is setup on the device
	public static bool IsEmailAvailable(){
		return EtceteraBinding.isEmailAvailable();
	}
			
	// Checks to see if SMS is available on the current device
	public static bool IsSMSAvailable(){
		return EtceteraBinding.isSMSAvailable();
	}
			
	// Opens the mail composer with the given information
	public static void ShowMailComposer( string toAddress, string subject, string body, bool isHTML ){
		EtceteraBinding.showMailComposer(toAddress,subject,body,isHTML);
	}
			
	// Opens the mail composer with a screenshot of the current state of the game attached
	public static IEnumerator ShowMailComposerWithScreenshot( string toAddress, string subject, string body, bool isHTML ){
		return EtceteraBinding.showMailComposerWithScreenshot(toAddress,subject,body,isHTML);
	}
			
	// Opens the mail composer with the given attachment file. The attachment data must be stored in a file on disk.
	public static void ShowMailComposerWithAttachment (string filePathToAttachment, string attachmentMimeType, string attachmentFilename, string toAddress, string subject, string body, bool isHTML ){
		EtceteraBinding.showMailComposerWithAttachment(filePathToAttachment,attachmentMimeType, attachmentFilename, toAddress, subject, body, isHTML);
	}
			
	// Opens the mail composer with the given attachment
	public static void ShowMailComposerWithAttatchment( byte[] attachmentData, string attachmentMimeType, string attachmentFilename, string toAddress, string subject, string body, bool isHTML ){
		EtceteraBinding.showMailComposerWithAttachment( attachmentData,attachmentMimeType,attachmentFilename, toAddress,subject, body,isHTML );
	}
			
	// Opens the sms composer with the given body and optional recipients
	public void ShowSmsComposer( string body ){
		EtceteraBinding.showSMSComposer(body);
	}
			
	public  void ShowSmsComposer( string[] recipients, string body ){
		EtceteraBinding.showSMSComposer(recipients,body );
	}
			
	// Shows a simple native spinner.  You must call hideActivityView to hide it
	public void ShowActivityView(){
		EtceteraBinding.showActivityView();
	}
			
	// Hides any activity views that are showing
	public  void HideActivityView(){
		EtceteraBinding.hideActivityView();
	}
			
	// Shows a bezel activity view with a label
	public  void ShowBezelActivityViewWithLabel( string label ){
		EtceteraBinding.showBezelActivityViewWithLabel(label);
	}
			
	// Shows a bezel activity view with a label and image
	public void ShowBezelActivityViewWithImage( string label, string imagePath ){
		EtceteraBinding.showBezelActivityViewWithImage( label, imagePath);
	}
			
	// Opens the ask for review dialogue only if the game has been launched \'launchCount\' times, the user did not request to not
	// be asked again, the user has not previously reviewed this version of the game and at least \'hoursBetweenPrompts\' has passed
	// since the last prompt. Note that the prompt will not be shown for at least 48 hours after this method is first called! Use the
	// other askForReview variant in the case where you want to use your own schedule or show the prompt immediately.
	public void AskForReview( int launchCount, float hoursBetweenPrompts, string title, string message){
		if (ITunesAppStoreId != null )
			AskForReview(launchCount,hoursBetweenPrompts,title,message,ITunesAppStoreId);
		else
			Debug.Log("AskForReview::ITunesAppStoreId is null");
	}

	public  void AskForReview( int launchCount, float hoursBetweenPrompts, string title, string message, string iTunesAppId ){
		EtceteraBinding.askForReview(launchCount,hoursBetweenPrompts,title, message, iTunesAppId );
	}
			
	// Opens the ask for review dialogue immediately
	public  void AskForReview( string title, string message){
		if (ITunesAppStoreId != null )
			AskForReview( title, message, ITunesAppStoreId );
		else
			Debug.Log("AskForReview::ITunesAppStoreId is null");
	}

	public void AskForReview( string title, string message, string iTunesAppId ){
		EtceteraBinding.askForReview( title, message, iTunesAppId );
	}
			
	// Opens App Store to the specified appId
	public  void OpenAppStoreReviewPage(){
		if (ITunesAppStoreId != null )
			OpenAppStoreReviewPage(ITunesAppStoreId);
		else
			Debug.Log("OpenAppStoreReviewPage::ITunesAppStoreId is null");

	}
	public void OpenAppStoreReviewPage(string iTunesAppId ){
		EtceteraBinding.openAppStoreReviewPage(iTunesAppId);
	}
			
	// Sets the position from which the popover for prompting for a photo will show when on an iPad
	public void SetPopoverPointOnIpad( float xPos, float yPos ){
		EtceteraBinding.setPopoverPoint( xPos, yPos );
	}
			
	// for backwards compatibility
	public  void PromptForPhoto( float scaledToSize ){
		EtceteraBinding.promptForPhoto(scaledToSize);
	}
			
	public  void PromptForPhoto( float scaledToSize, PhotoPromptType promptType ){
		EtceteraBinding.promptForPhoto(scaledToSize,promptType );
	}
			
	// Prompts the user to either take a photo or choose from the photo library.  scaledToSize should be set
	// less than 1.0f in most cases to avoid getting a huge image from the camera or photo library unless you plan to resize
	// the image later. jpegCompression should be between 0 - 1. Photos are automatically rotated to match the EXIF data.
	// When complete either the imagePickerCancelledEvent or imagePickerChoseImageEvent event will fire.
	public  void PromptForPhoto( float scaledToSize, PhotoPromptType promptType, float jpegCompression, bool allowsEditing ){
		EtceteraBinding.promptForPhoto(scaledToSize, promptType, jpegCompression, allowsEditing);
	}
			
	// Prompts the user to choose one or more photos from the photo library. scaledToSize should be set
	// less than 1.0f in most cases to avoid getting a huge image from the camera or photo library unless you plan to resize
	// the image later. jpegCompression should be between 0 - 1. Photos are automatically rotated to match the EXIF data.
	// Fires the same events as promptForPhoto.
	public void PromptForMultiplePhotos( int maxNumberOfPhotos, float scaledToSize, float jpegCompression = 0.8f ){
		EtceteraBinding.promptForMultiplePhotos( maxNumberOfPhotos, scaledToSize, jpegCompression);
	}
			
	// Resizes and optionally crops the image at the given path. Note that the image will be saved as a JPEG to keep EXIF data intact if possible.
	public void ResizeImageAtPath( string filePath, float width, float height ){
		EtceteraBinding.resizeImageAtPath( filePath, width, height);
	}
			
	// Gets the size of the image at the given path.  Returns 0,0 for invalid paths
	public Vector2 GetImageSize( string filePath ){
		return EtceteraBinding.getImageSize(filePath);
	}
			
	// Writes the given image to the users photo album
	public  void SaveImageToPhotoAlbum( string filePath ){
		EtceteraBinding.saveImageToPhotoAlbum(filePath);
	}
			
	// Sets the Urban Airship credentials and optionally the alias. Set these before calling registerForRemoteNotifications
	public  void SetUrbanAirshipCredentials( string appKey, string appSecret ){
		EtceteraBinding.setUrbanAirshipCredentials(appKey,appSecret);
	}
			
	public  void SetUrbanAirshipCredentials( string appKey, string appSecret, string alias ){
		EtceteraBinding.setUrbanAirshipCredentials(appKey,appSecret,alias);
	}
			
	// Sets the Push.io credentials and optionally the PushIO categories. Set these before calling registerForRemoteNotifications
	public  void SetPushIOCredentials( string apiKey ){
		EtceteraBinding.setPushIOCredentials(apiKey);
	}
			
	public  void SetPushIOCredentials( string apiKey, string[] categories ){
		EtceteraBinding.setPushIOCredentials(apiKey,categories);
	}
			
	// Registers the deviceToken with GameThrive. Note that a GameThrive app ID is required to use GameThrive.
	public  IEnumerator RegisterDeviceWithGameThrive( string gameThriveAppId, string deviceToken, Dictionary<string,string> additionalParameters = null, Action<WWW> completionHandler = null ){
		return EtceteraBinding.registerDeviceWithGameThrive(gameThriveAppId,deviceToken,  additionalParameters,  completionHandler);
	}
			
	// Registers the game for remote (push) notifications. types is a bitmask
	public  void RegisterForRemoteNotifcations( P31RemoteNotificationType types ){
		EtceteraBinding.registerForRemoteNotifications(types);
	}
			
	// Gets the bitmasked notification types the user has registered for
	public  P31RemoteNotificationType GetEnabledRemoteNotificationTypes(){
		return  EtceteraBinding.getEnabledRemoteNotificationTypes();
	}
			
	// Gets the current application badge count
	public  int GetBadgeCount(){
		return EtceteraBinding.getBadgeCount();
	}
			
	// Sets the current application badge count
	public  void SetBadgeCount( int badgeCount ){
		EtceteraBinding.setBadgeCount(badgeCount);
	}
			
	// Gets the current UIApplication\'s status bar orientation
	public  UIInterfaceOrientation GetStatusBarOrientation(){
		return EtceteraBinding.getStatusBarOrientation();
	}
			
	// Shows the inline web view. Remember, iOS uses points not pixels for positioning and layout!
	public  void InlineWebViewShow( int x, int y, int width, int height ){
		EtceteraBinding.inlineWebViewShow(x,y,width,height);
	}
			
	// Closes the inline web view
	public  void InlineWebViewClose(){
		EtceteraBinding.inlineWebViewClose();
	}
			
	// Sets the current url for the inline web view
	public  void InlineWebViewSetUrl( string url ){
		EtceteraBinding.inlineWebViewSetUrl(url);
	}
			
	// Sets the current frame for the inline web view
	public  void InlineWebViewSetFrame( int x, int y, int width, int height ){
		EtceteraBinding.inlineWebViewSetFrame(x,y,width,height);
	}

}