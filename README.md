# Most-Popular-Names
Alexa Skill - Most Popular Names 

This is a simple fully functional Amazon Alexa Skills Application. In addition 
to the code, you will need to fill out the Amazon Developers Wizard for Alexa 
Skills. Do not confuse an Alexa Skill with the standard Amazon application. 
The Alexa Skills Wizard is located under the Alexa tab and you must be signed 
in to access the “Get Started:” link.

After filling out the Alexa Skills Wizard, you will be provided with an 
Application Id, which will need to be set in the AppSettings section of the 
web.config file.

/ Most-Popular-Names / PopularNames / Models / 
AlexaRequestModel.cs

The source code is a Microsoft C# Web API 2.0 project with a
couple special files. Amazon only sends requests to a single endpoint, but your
skill will most likely support many different intents. An intent is Amazon’s
word for type of request. If you were creating a game you 
provided by Amazon. This would also be the place to create would need several
different intents to interact within the game. To command the player to travel
north would require an intend for moment and take a slot for direction. To
attack an enemy would require a completely different type of intent with several
different options including which weapon to use and which enemy to attack. The
Alexa Request is static in structure except for the slots. To handle the custom
slots we use a dynamic type and by knowing what slots are supported by each
intent type we can query for the slots information we need. To make handling
slots easier, a Slots method to provide additional slots support.

/ Most-Popular-Names / PopularNames / Models / 
AlexaResponseModel.cs

The response model is simple with the main property being the output property. 
In addition to output you will also need to populate the text for the card 
which is displayed within the Alexa Mobile Application on your phone.

/ Most-Popular-Names / PopularNames / Handlers / 
CertificateValidationHandler.cs

Amazon requires strict validation and during the submission
phase Amazon will attempt to send invalid values including invalid
certificates, or values outside an acceptable range. The CertificateValidationHandler
handles these important security features. The code for the handler was largely
taken from the Plural Sight Course, “Developing Alexa Skills for Amazon Echo”
by Walter Quesada. I considered some additional refactoring of the handler but
as mentioned in the course, caching certificates isn’t likely a good idea as
they expire and can be updated at any time.

*** The handler must be registered in the webapiconfig.cs file. 

config.MessageHandlers.Add(new CertificateValidationHandler());

/ Most-Popular-Names / PopularNames / Filters / 
AlexaAuthFilter.cs

The last file is the AlexaAuthFilter which might seem strange to use an action 
filter for authentication when Web API provides an authentication filter but 
Amazon doesn’t provide the application id in the request headers, instead it’s 
part of the Json request and therefore we need to be able to access the model 
that will be provided to the controller’s action method. 

The filter is primarily validating the Application Ida session if you
wanted to allow multiple inputs without having to invoke the skill name repeatedly.

*** The [AlexaAuthFilter] must be placed on the controller’s
action method, class, or registered globally in the webapiconfig.cs file.

