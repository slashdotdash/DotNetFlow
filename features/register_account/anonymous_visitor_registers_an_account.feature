Feature: Anonymous visitor registers an account
  In order to gain reputation within the site
  As a visitor to the site
  I want to register an account

  Scenario: Successful registration
    Given I am on the registration page
	When I complete the required fields for registration
	And I press "Register Now"
	Then I should be on the home page
	And I should be logged in
    #And I should see the message "Thank-you for registering an account."

  Scenario: Attempts to register with an already registered email
    Given an account has been registered with an email address
	When I am on the registration page
	And I complete the required fields for registration with an existing email
	And I press "Register Now"
	Then I should see the error message "Email address has already been registered"