When /^I enter my username and password$/ do
  @username = @command.username
  @password = @command.password
  
  When %{I fill in "Username or E-mail" with "#{@username}"}
  When %{I fill in "Password" with "#{@password}"}
end

When /^I enter my email address and password$/ do
  @email = @command.email
  @password = @command.password
  
  When %{I fill in "Username or E-mail" with "#{@email}"}
  When %{I fill in "Password" with "#{@password}"}
end

When /^I enter my email address and the wrong password$/ do
  @email = @command.email
  
  When %{I fill in "Username or E-mail" with "#{@email}"}
  When %{I fill in "Password" with "incorrect"}
end

When /^I enter an incorrect username and password$/ do
  @username = @command.username
  @password = @command.password
  
  When %{I fill in "Username or E-mail" with "x#{@username}x"}
  When %{I fill in "Password" with "#{@password}"}
end

Then /^I should be logged in$/ do
  Then %{I should see "Welcome #{@username}."}
end

Then /^I should not be logged in$/ do
  Then %{I should see "Welcome Guest."}
end

Then /^I should see the login failed error message$/ do
  Then %{I should see "Login failed, please check your username or e-mail address and password and try again."}
end