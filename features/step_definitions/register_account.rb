When /^I complete the required fields for registration$/ do
  @name = Faker::Name.name
  @email = Faker::Internet.email
	
  When %{I fill in "Full Name" with "#{@name}"}
  When %{I fill in "E-mail" with "#{@email}"}
  When %{I fill in "Password" with "password"}
end

Then /^I should be logged in$/ do
  Then %{I should see "Welcome #{@name}."}  
end