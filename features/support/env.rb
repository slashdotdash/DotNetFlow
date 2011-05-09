require 'rubygems'
require 'capybara'
require 'capybara/dsl'

include Capybara

#Capybara.log 'test'

#Capybara.default_driver = :culerity
Capybara.default_driver = :selenium
Capybara.javascript_driver = :akephalos
Capybara.use_default_driver
Capybara.app_host = 'http://localhost:3000'

# Capybara defaults to XPath selectors rather than Webrat's default of CSS3. In
# order to ease the transition to Capybara we set the default here. If you'd
# prefer to use XPath just remove this line and adjust any selectors in your
# steps to use the XPath syntax.
Capybara.default_selector = :css

#puts Capybara.default_driver