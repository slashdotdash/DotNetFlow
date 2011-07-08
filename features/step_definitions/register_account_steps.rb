When /^I complete the required fields for registration$/ do
  @name = Faker::Name.name
  @username = @name.strip.gsub(/\s/, '')
  @email = Faker::Internet.email

  When %{I fill in "Full Name" with "#{@name}"}
  When %{I fill in "Username" with "#{@username}"}
  When %{I fill in "E-mail" with "#{@email}"}
  When %{I fill in "Password" with "password"}
end

Then /^I should be logged in$/ do
  Then %{I should see "Welcome #{@name}."}
end

Given /^an account has been registered with an email address$/ do
  Given %{I am on the registration page}
  Given %{I complete the required fields for registration}
  Given %{I press "Register Now"}
end

When /^I complete the required fields for registration with an existing email$/ do
  @name = Faker::Name.name
  @username = @name.strip.gsub(/\s/, '')
  @existing_email = @email
  
  When %{I fill in "Full Name" with "#{@name}"}
  When %{I fill in "Username" with "#{@username}"}	
  When %{I fill in "E-mail" with "#{@existing_email}"}
  When %{I fill in "Password" with "password"}
end

Then /^I should see the error message "([^"]*)"$/ do |message|
  Then %{I should see "#{message}"} 
end